-- =============================================
-- QUICK DIAGNOSTIC SCRIPT
-- Run this to check if your database is set up correctly
-- =============================================

USE omlajue_ecommerce;

-- 1. Check if database exists and is selected
SELECT DATABASE() as CurrentDatabase;

-- 2. Count products
SELECT 
    COUNT(*) as TotalProducts,
    COUNT(CASE WHEN IsFeatured = 1 THEN 1 END) as FeaturedProducts,
    COUNT(CASE WHEN IsActive = 1 THEN 1 END) as ActiveProducts
FROM products;

-- 3. Count product images
SELECT 
    COUNT(*) as TotalImages,
    COUNT(CASE WHEN IsPrimary = 1 THEN 1 END) as PrimaryImages
FROM productimages;

-- 4. Show products and their images
SELECT 
    p.ProductId,
    p.ProductName,
    p.IsFeatured,
    pi.ImageUrl,
    pi.IsPrimary
FROM products p
LEFT JOIN productimages pi ON p.ProductId = pi.ProductId
WHERE p.IsFeatured = 1
ORDER BY p.ProductId
LIMIT 10;

-- 5. Find products WITHOUT images (should be empty!)
SELECT 
    p.ProductId,
    p.ProductName,
    'NO IMAGE!' as Status
FROM products p
LEFT JOIN productimages pi ON p.ProductId = pi.ProductId
WHERE pi.ImageId IS NULL;

-- 6. Check image paths format
SELECT 
    ImageUrl,
    CASE 
        WHEN ImageUrl LIKE '/Content/images/products/%' THEN '? Correct Format'
        WHEN ImageUrl LIKE '/images/products/%' THEN '? Missing /Content'
        ELSE '? Wrong Format'
    END as PathStatus
FROM productimages
GROUP BY ImageUrl
LIMIT 5;

-- 7. Summary Report
SELECT 
    '=== DATABASE HEALTH CHECK ===' as Report
UNION ALL
SELECT CONCAT('Products in DB: ', COUNT(*)) FROM products
UNION ALL
SELECT CONCAT('Featured Products: ', COUNT(*)) FROM products WHERE IsFeatured = 1
UNION ALL
SELECT CONCAT('Product Images: ', COUNT(*)) FROM productimages
UNION ALL
SELECT CONCAT('Products Missing Images: ', COUNT(*)) 
FROM products p 
LEFT JOIN productimages pi ON p.ProductId = pi.ProductId 
WHERE pi.ImageId IS NULL;

-- =============================================
-- EXPECTED RESULTS:
-- - Total Products: 30
-- - Featured Products: 18
-- - Total Images: 30
-- - Primary Images: 30
-- - Products Missing Images: 0
-- - All image paths start with /Content/images/products/
-- =============================================
