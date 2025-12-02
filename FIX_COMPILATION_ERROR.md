# Fix Compilation Error - SearchQuery Not Found

## The Issue
You're getting a compilation error: `'HomeViewModel' does not contain a definition for 'SearchQuery'`

This is because:
1. The HomeViewModel.cs was updated but not compiled
2. Temporary ASP.NET files are cached

## Quick Fix (Choose ONE method)

### Method 1: Clean and Rebuild in Visual Studio
1. **Stop the application** (if running)
2. In Visual Studio menu: **Build ? Clean Solution**
3. Wait for it to complete
4. Then: **Build ? Rebuild Solution**
5. **Run the application** (F5)

### Method 2: Manual Clean (If Method 1 doesn't work)
1. **Stop the application**
2. Close Visual Studio
3. Navigate to your project folder:
   ```
   C:\Users\Jacob Enero\source\repos\Omlajue_Ecommerce\
   ```
4. Delete these folders:
   - `Omlajue_Ecommerce\bin`
   - `Omlajue_Ecommerce\obj`
5. Delete temporary ASP.NET files:
   - Navigate to: `C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\`
   - Delete all folders inside
   - Or run this in Command Prompt (as Administrator):
     ```cmd
     del /s /q "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\*.*"
     del /s /q "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Temporary ASP.NET Files\*.*"
     ```
6. Open Visual Studio again
7. **Build ? Rebuild Solution**
8. Run the application

### Method 3: Quick PowerShell Script
Run this in PowerShell (as Administrator):

```powershell
# Stop IIS Express
Get-Process | Where-Object {$_.ProcessName -like "*iisexpress*"} | Stop-Process -Force

# Clear temp files
Remove-Item "C:\Users\Jacob Enero\source\repos\Omlajue_Ecommerce\Omlajue_Ecommerce\bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item "C:\Users\Jacob Enero\source\repos\Omlajue_Ecommerce\Omlajue_Ecommerce\obj" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\*" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Temporary ASP.NET Files\*" -Recurse -Force -ErrorAction SilentlyContinue

Write-Host "Cleanup complete! Now rebuild in Visual Studio." -ForegroundColor Green
```

## Verify the Fix Worked

After rebuilding, you should see:
- ? No compilation errors
- ? Home page loads correctly
- ? For **logged-in users**: Filter section appears with all fields
- ? For **guest users**: Landing page with featured products

## What Was Changed

The `HomeViewModel.cs` now includes these properties for filtering:
```csharp
public int? SelectedBrandId { get; set; }
public int? SelectedCategoryId { get; set; }
public string SelectedGender { get; set; }
public decimal? MinPrice { get; set; }
public decimal? MaxPrice { get; set; }
public string SearchQuery { get; set; }
public string SortBy { get; set; }
```

The `HomeController.cs` now:
- Accepts filter parameters
- Loads ALL products for logged-in users
- Applies filtering and sorting
- Returns filtered results

## If Still Not Working

1. Check the `HomeViewModel.cs` file contains the SearchQuery property
2. Check `Web.config` has correct compilation settings
3. Restart Visual Studio completely
4. Try "Run as Administrator"

## Need Help?

If the error persists after trying all methods above, let me know and I'll provide additional troubleshooting steps.

---
**Remember**: Always stop the application before cleaning/rebuilding!
