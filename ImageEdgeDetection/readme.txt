/*
** TP 1 : Summary of what has been achieved
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
Added Bitmap ImageFilterResult variable in order to undo Edge Detection if an image filter was applied
Replaced if/else by switch/case in ApplyEdgeDetection
Redundant function grouping -> ApplyGreyScale
Replace the preview image with a new loaded image by applying the existing filters
Add comments

/*
** To be done in future improvements
*/
Optimize the ConvolutionFilter and create one method
Improve user experience with customized design
Refactor MainForm in order to prepare original size image for saving in order to avoid applying filters for display and re-apply them for save action

/*
** TP 2 
*/
Created test project and test classes
Created test for black and white image filter : ImageFilters.BlackWhite(img)
MainForm is not tested because setting methods to public static is not a good practice
Adapted BlackWhiteTest method according to Dominique's feedback
Added Sobel3x3FilterTest method in EdgeFiltersTest class (bitmap comparison : the reference image was obtained with this software,
but must be obtained by another way in order to complete unit test requirements. Need the support of an image processing expert.)
Added RainbowTest methods 
Corrected RainbowFilter method for 100% code coverage
Added exceptions test for RainbowFilter in ImageFiltersTest
Added exceptions test for CopyToSquare method in EdgeFiltersTest
Added test to check the Matrix for Laplacian3x3, Sorbel3x3Vertical
For Laplacian3x3Filter compare an original image after it has been filtered from another software (https://www.wolframalpha.com).
Added test for ApplyGreyScale()

Not covered results:
Exception tests cannot cover the entire code, because when an exception is thrown out, the method is discarded.
In other words, the test does not go through all the blocks because it comes out of the method.

For example: 
		ApplyFilter_InvalidArguments_ReturnsArgumentException()	1	33.33%	2	66.67%
		RainbowFilter_ArgumentIsNull_ReturnsNullReferenceException()	1	33.33%	2	66.67%
		ConvolutionFilter_ImageIsNull_ReturnsException()	2	40.00%	3	60.00%
		ApplyGreyScale_ByteArrayOutOfBounds_ReturnsIndexOutOfRangeException()	2	66.67%	1	33.33%
		CopyToSquareCanvas_PropertiesAreNullOrEmpty_ReturnException()	2	66.67%	1	33.33%
