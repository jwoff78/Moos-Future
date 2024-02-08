namespace System.Runtime
{
    internal static class CachedInterfaceDispatch
    {
        /*
        [RuntimeExport("RhpCidResolve")]
        private unsafe static IntPtr RhpCidResolve(IntPtr callerTransitionBlockParam, IntPtr pCell)
        {
            IntPtr locationOfThisPointer = callerTransitionBlockParam + TransitionBlock.GetThisOffset();
            object pObject = Unsafe.As<IntPtr, object>(ref *(IntPtr*)(void*)locationOfThisPointer);
            return RhpCidResolve_Worker(pObject, pCell);
        }

        private unsafe static IntPtr RhpCidResolve_Worker(object pObject, IntPtr pCell)
        {
            InternalCalls.RhpGetDispatchCellInfo(pCell, out var cellInfo);
            IntPtr pTargetCode = RhResolveDispatchWorker(pObject, (void*)pCell, ref cellInfo);
            if (pTargetCode != IntPtr.Zero)
            {
                if (!pObject.GetMethodTable()->IsIDynamicInterfaceCastable)
                {
                    return InternalCalls.RhpUpdateDispatchCellCache(pCell, pTargetCode, pObject.GetMethodTable(), ref cellInfo);
                }
                return pTargetCode;
            }
            //EH.FallbackFailFast(RhFailFastReason.InternalError, null);
            return IntPtr.Zero;
        }

        [RuntimeExport("RhpResolveInterfaceMethod")]
        private unsafe static IntPtr RhpResolveInterfaceMethod(object pObject, IntPtr pCell)
        {
            if (pObject == null)
            {
                return IntPtr.Zero;
            }
            MethodTable* pInstanceType = pObject.GetMethodTable();
            IntPtr pTargetCode = InternalCalls.RhpSearchDispatchCellCache(pCell, pInstanceType);
            if (pTargetCode == IntPtr.Zero)
            {
                pTargetCode = RhpCidResolve_Worker(pObject, pCell);
            }
            return pTargetCode;
        }

        [RuntimeExport("RhResolveDispatch")]
        private unsafe static IntPtr RhResolveDispatch(object pObject, EETypePtr interfaceType, ushort slot)
        {
            DispatchCellInfo cellInfo = default(DispatchCellInfo);
            cellInfo.CellType = DispatchCellType.InterfaceAndSlot;
            cellInfo.InterfaceType = interfaceType;
            cellInfo.InterfaceSlot = slot;
            return RhResolveDispatchWorker(pObject, null, ref cellInfo);
        }

        [RuntimeExport("RhResolveDispatchOnType")]
        private unsafe static IntPtr RhResolveDispatchOnType(EETypePtr instanceType, EETypePtr interfaceType, ushort slot, EETypePtr* pGenericContext)
        {
            MethodTable* pInstanceType = instanceType.ToPointer();
            MethodTable* pInterfaceType = interfaceType.ToPointer();
            return DispatchResolve.FindInterfaceMethodImplementationTarget(pInstanceType, pInterfaceType, slot, (MethodTable**)pGenericContext);
        }

        private unsafe static IntPtr RhResolveDispatchWorker(object pObject, void* cell, ref DispatchCellInfo cellInfo)
        {
            MethodTable* pInstanceType = pObject.GetMethodTable();
            if (cellInfo.CellType == DispatchCellType.InterfaceAndSlot)
            {
                MethodTable* pResolvingInstanceType = pInstanceType;
                IntPtr pTargetCode = DispatchResolve.FindInterfaceMethodImplementationTarget(pResolvingInstanceType, cellInfo.InterfaceType.ToPointer(), cellInfo.InterfaceSlot, null);
                if (pTargetCode == IntPtr.Zero && pInstanceType->IsIDynamicInterfaceCastable)
                {
                    pTargetCode = ((delegate*<object, MethodTable*, ushort, IntPtr>)(void*)pInstanceType->GetClasslibFunction(ClassLibFunctionId.IDynamicCastableGetInterfaceImplementation))(pObject, cellInfo.InterfaceType.ToPointer(), cellInfo.InterfaceSlot);
                }
                return pTargetCode;
            }
            if (cellInfo.CellType == DispatchCellType.VTableOffset)
            {
                return *(IntPtr*)((byte*)pInstanceType + cellInfo.VTableOffset);
            }
            //EH.FallbackFailFast(RhFailFastReason.InternalError, null);
            return IntPtr.Zero;
        }

        */
    }

}
