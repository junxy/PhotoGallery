using System;
namespace Microsoft.Internal.Web.Utils
{
	internal static class ExceptionHelper
	{
		internal static ArgumentException CreateArgumentNullOrEmptyException(string paramName)
		{
			return new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, paramName);
		}
	}
}
