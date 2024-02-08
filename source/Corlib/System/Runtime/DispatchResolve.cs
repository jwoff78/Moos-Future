using Internal.Runtime;

namespace System.Runtime
{
    /* 
     * TODO: URGENT: Dispatch resolve simply doesn't let us compile, I don't know why yet
    internal static class DispatchResolve
    {
        public unsafe static IntPtr FindInterfaceMethodImplementationTarget(MethodTable* pTgtType, MethodTable* pItfType, ushort itfSlotNumber, MethodTable** ppGenericContext)
        {
            DynamicModule* dynamicModule = pTgtType->DynamicModule;
            if (dynamicModule != null)
            {
                delegate*<MethodTable*, MethodTable*, ushort, IntPtr> resolver = dynamicModule->DynamicTypeSlotDispatchResolve;
                if (resolver != (delegate*<MethodTable*, MethodTable*, ushort, IntPtr>)null)
                {
                    return resolver(pTgtType, pItfType, itfSlotNumber);
                }
            }
            MethodTable* pCur = pTgtType;
            if (pItfType->IsCloned)
            {
                pItfType = pItfType->CanonicalEEType;
            }
            bool fDoDefaultImplementationLookup = false;
            ushort implSlotNumber = default(ushort);
            while (true)
            {
                if (pCur != null)
                {
                    if (FindImplSlotForCurrentType(pCur, pItfType, itfSlotNumber, fDoDefaultImplementationLookup, &implSlotNumber, ppGenericContext))
                    {
                        if (implSlotNumber < pCur->NumVtableSlots)
                        {
                            return pTgtType->GetVTableStartAddress()[(int)implSlotNumber];
                        }
                        return implSlotNumber switch
                        {
                            ushort.MaxValue => throw pTgtType->GetClasslibException(ExceptionIDs.EntrypointNotFound),
                            65534 => throw pTgtType->GetClasslibException(ExceptionIDs.AmbiguousImplementation),
                            _ => pCur->GetSealedVirtualSlot((ushort)(implSlotNumber - pCur->NumVtableSlots)),
                        };
                    }
                    pCur = ((!pCur->IsArray) ? pCur->NonArrayBaseType : pCur->GetArrayEEType());
                }
                else
                {
                    if (fDoDefaultImplementationLookup)
                    {
                        break;
                    }
                    fDoDefaultImplementationLookup = true;
                    pCur = pTgtType;
                }
            }
            return IntPtr.Zero;
        }

        private unsafe static bool FindImplSlotForCurrentType(MethodTable* pTgtType, MethodTable* pItfType, ushort itfSlotNumber, bool fDoDefaultImplementationLookup, ushort* pImplSlotNumber, MethodTable** ppGenericContext)
        {
            bool fRes = false;
            if (!pItfType->IsInterface)
            {
                *pImplSlotNumber = itfSlotNumber;
                return pTgtType == pItfType;
            }
            if (pTgtType->HasDispatchMap)
            {
                bool fDoVariantLookup = false;
                fRes = FindImplSlotInSimpleMap(pTgtType, pItfType, itfSlotNumber, pImplSlotNumber, ppGenericContext, fDoVariantLookup, fDoDefaultImplementationLookup);
                if (!fRes)
                {
                    fDoVariantLookup = true;
                    fRes = FindImplSlotInSimpleMap(pTgtType, pItfType, itfSlotNumber, pImplSlotNumber, ppGenericContext, fDoVariantLookup, fDoDefaultImplementationLookup);
                }
            }
            return fRes;
        }

        private unsafe static bool FindImplSlotInSimpleMap(MethodTable* pTgtType, MethodTable* pItfType, uint itfSlotNumber, ushort* pImplSlotNumber, MethodTable** ppGenericContext, bool actuallyCheckVariance, bool checkDefaultImplementations)
        {
            MethodTable* pItfOpenGenericType = null;
            EETypeRef* pItfInstantiation = null;
            int itfArity = 0;
            GenericVariance* pItfVarianceInfo = null;
            bool fCheckVariance = false;
            bool fArrayCovariance = false;
            if (actuallyCheckVariance)
            {
                fCheckVariance = pItfType->HasGenericVariance;
                fArrayCovariance = pTgtType->IsArray;
                if (!fArrayCovariance && pTgtType->HasGenericVariance)
                {
                    int tgtEntryArity = (int)pTgtType->GenericArity;
                    GenericVariance* pTgtVarianceInfo = pTgtType->GenericVariance;
                    if (tgtEntryArity == 1 && *pTgtVarianceInfo == GenericVariance.ArrayCovariant)
                    {
                        fArrayCovariance = true;
                    }
                }
                if (fArrayCovariance && pItfType->IsGeneric)
                {
                    fCheckVariance = true;
                }
                if (!fCheckVariance)
                {
                    return false;
                }
            }
            bool fStaticDispatch = ppGenericContext != null;
            DispatchMap* pMap = pTgtType->DispatchMap;
            DispatchMap.DispatchMapEntry* i = (fStaticDispatch ? pMap->GetStaticEntry((int)(checkDefaultImplementations ? pMap->NumStandardStaticEntries : 0)) : pMap->GetEntry((int)(checkDefaultImplementations ? pMap->NumStandardEntries : 0)));
            DispatchMap.DispatchMapEntry* iEnd = (fStaticDispatch ? pMap->GetStaticEntry((int)(checkDefaultImplementations ? (pMap->NumStandardStaticEntries + pMap->NumDefaultStaticEntries) : pMap->NumStandardStaticEntries)) : pMap->GetEntry((int)(checkDefaultImplementations ? (pMap->NumStandardEntries + pMap->NumDefaultEntries) : pMap->NumStandardEntries)));
            while (i != iEnd)
            {
                if (i->_usInterfaceMethodSlot == itfSlotNumber)
                {
                    MethodTable* pCurEntryType = pTgtType->InterfaceMap[(int)i->_usInterfaceIndex].InterfaceType;
                    if (pCurEntryType->IsCloned)
                    {
                        pCurEntryType = pCurEntryType->CanonicalEEType;
                    }
                    if (pCurEntryType == pItfType)
                    {
                        *pImplSlotNumber = i->_usImplMethodSlot;
                        if (fStaticDispatch)
                        {
                            *ppGenericContext = GetGenericContextSource(pTgtType, i);
                        }
                        return true;
                    }
                    if (fCheckVariance && ((fArrayCovariance && pCurEntryType->IsGeneric) || pCurEntryType->HasGenericVariance))
                    {
                        if (pItfOpenGenericType == null)
                        {
                            pItfOpenGenericType = pItfType->GenericDefinition;
                            itfArity = (int)pItfType->GenericArity;
                            pItfInstantiation = pItfType->GenericArguments;
                            pItfVarianceInfo = pItfType->GenericVariance;
                        }
                        MethodTable* pCurEntryGenericType = pCurEntryType->GenericDefinition;
                        if (pItfOpenGenericType == pCurEntryGenericType)
                        {
                            EETypeRef* pCurEntryInstantiation = pCurEntryType->GenericArguments;
                            if (TypeCast.TypeParametersAreCompatible(itfArity, pCurEntryInstantiation, pItfInstantiation, pItfVarianceInfo, fArrayCovariance, null))
                            {
                                *pImplSlotNumber = i->_usImplMethodSlot;
                                if (fStaticDispatch)
                                {
                                    *ppGenericContext = GetGenericContextSource(pTgtType, i);
                                }
                                return true;
                            }
                        }
                    }
                }
                i = (fStaticDispatch ? ((DispatchMap.DispatchMapEntry*)((byte*)i + sizeof(DispatchMap.StaticDispatchMapEntry))) : (i + 1));
            }
            return false;
        }

        private unsafe static MethodTable* GetGenericContextSource(MethodTable* pTgtType, DispatchMap.DispatchMapEntry* pEntry)
        {
            ushort usEncodedValue = ((DispatchMap.StaticDispatchMapEntry*)pEntry)->_usContextMapSource;
            return usEncodedValue switch
            {
                0 => null,
                1 => pTgtType,
                _ => pTgtType->InterfaceMap[usEncodedValue - 2].InterfaceType,
            };
        }
    }

    */
}
