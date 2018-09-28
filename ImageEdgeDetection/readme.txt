/*
** Summary of what has been achieved
*/
Imported ImageFilters.cs class
Added new DropDownList in MainForm.Designer.cs (cmbFilters)
Change MainForm title
Add tips for the user
Implemented selected item event handler for cmbFilters in MainForm.cs
Renamed ApplyFilter method -> ApplyEdgeDetection
Added ApplyImageFilter method
Added bool imageFilter to check if an image filter was applied before edge detection an choose right selected source
Implemented cmbFilters de-activation if edge detection is applied
Added Bitmap ImageFilterResult variable in order to undo Edge Detection if a n image filter was applied
Replaced if/else by switch/case in ApplyEdgeDetection
Redundant function grouping -> ApplyGreyScale
Replace the preview image with a new loaded image by applying the existing filters
Add comments

/*
** To be done in future improvements
*/
Optimize the ConvolutionFilter and create one method
Imporove user experience with customized design