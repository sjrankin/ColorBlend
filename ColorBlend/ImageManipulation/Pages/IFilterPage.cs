namespace ColorBlend
{
    /// <summary>
    /// Interface for filter UI pages.
    /// </summary>
    interface IFilterPage
    {
        /// <summary>
        /// Clear any WriteableBitmaps in the page.
        /// </summary>
        void Clear ();

        /// <summary>
        /// Emit a pipeline stage with the settings defined in this page.
        /// </summary>
        /// <returns></returns>
        StageBase EmitPipelineStage ();
    }
}
