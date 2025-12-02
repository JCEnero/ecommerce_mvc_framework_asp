-- =============================================
-- FIX EXISTING IMAGE URLS IN DATABASE
-- Run this if you already imported data with wrong paths
-- =============================================

USE omlajue_ecommerce;

-- =============================================
-- UPDATE IMAGE URLS TO CORRECT PATH
-- Change from /images/products/ to /Content/images/products/
-- =============================================

UPDATE productimages 
SET ImageUrl = REPLACE(ImageUrl, '/images/products/', '/Content/images/products/')
WHERE ImageUrl LIKE '/images/products/%';

-- =============================================
-- VERIFY THE FIX
-- =============================================

-- Check updated paths
SELECT 
    ProductId,
    ImageUrl,
    CASE 
        WHEN ImageUrl LIKE '/Content/images/products/%' THEN '? Correct'
        ELSE '? Wrong'
    END as Status
FROM productimages
ORDER BY ProductId;

-- Count correct vs wrong
SELECT 
    CASE 
        WHEN ImageUrl LIKE '/Content/images/products/%' THEN 'Correct Path'
        ELSE 'Wrong Path'
    END as PathType,
    COUNT(*) as Count
FROM productimages
GROUP BY PathType;

-- =============================================
-- EXPECTED RESULT
-- All 30 images should have path: /Content/images/products/watchX.jpg
-- =============================================
