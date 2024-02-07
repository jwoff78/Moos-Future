namespace System.Runtime
{
    internal struct DispatchCellInfo
    {
        public DispatchCellType CellType;

        public EETypePtr InterfaceType;

        public ushort InterfaceSlot;

        public byte HasCache;

        public uint MetadataToken;

        public uint VTableOffset;
    }
}
