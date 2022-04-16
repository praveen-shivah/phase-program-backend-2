namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class CustomerCustomField : BaseEntity
    {
        public string CustomfieldId { get; set; }

        public string DataType { get; set; }

        public bool EditOnPortal { get; set; }

        public bool EditOnStore { get; set; }

        public int Index { get; set; }

        public bool IsActive { get; set; }

        public bool IsDependentField { get; set; }

        public string Label { get; set; }

        public string Placeholder { get; set; }

        public string SearchEntity { get; set; }

        public string SelectedOptionId { get; set; }

        public bool ShowInAllPdf { get; set; }

        public bool ShowInPortal { get; set; }

        public bool ShowInStore { get; set; }

        public bool ShowOnPdf { get; set; }

        public string Value { get; set; }

        public string ValueFormatted { get; set; }
    }
}