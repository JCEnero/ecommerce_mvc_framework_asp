-- =============================================
-- MANUALLY UPDATE IMAGE URLS FOR ALL PRODUCTS
-- Change each product to use correct watch image
-- =============================================

USE omlajue_ecommerce;

-- =============================================
-- OPTION 1: UPDATE ALL IMAGES TO USE watch1-18 FORMAT
-- This replaces old image names with watchX.jpg format
-- =============================================

-- First, let's see what we have currently
SELECT 
    ProductId,
    ImageUrl as CurrentPath,
    CASE ProductId
        WHEN 1 THEN '/Content/images/products/watch1.jpg'
        WHEN 2 THEN '/Content/images/products/watch2.jpg'
        WHEN 3 THEN '/Content/images/products/watch3.jpg'
        WHEN 4 THEN '/Content/images/products/watch4.jpg'
        WHEN 5 THEN '/Content/images/products/watch5.jpg'
        WHEN 6 THEN '/Content/images/products/watch6.jpg'
        WHEN 7 THEN '/Content/images/products/watch7.jpg'
        WHEN 8 THEN '/Content/images/products/watch8.jpg'
        WHEN 9 THEN '/Content/images/products/watch9.jpg'
        WHEN 10 THEN '/Content/images/products/watch10.jpg'
        WHEN 11 THEN '/Content/images/products/watch11.jpg'
        WHEN 12 THEN '/Content/images/products/watch12.jpg'
        WHEN 13 THEN '/Content/images/products/watch13.jpg'
        WHEN 14 THEN '/Content/images/products/watch14.jpg'
        WHEN 15 THEN '/Content/images/products/watch15.jpg'
        WHEN 16 THEN '/Content/images/products/watch16.jpg'
        WHEN 17 THEN '/Content/images/products/watch17.jpg'
        WHEN 18 THEN '/Content/images/products/watch18.jpg'
        WHEN 19 THEN '/Content/images/products/watch1.jpg'
        WHEN 20 THEN '/Content/images/products/watch2.jpg'
        WHEN 21 THEN '/Content/images/products/watch3.jpg'
        WHEN 22 THEN '/Content/images/products/watch4.jpg'
        WHEN 23 THEN '/Content/images/products/watch5.jpg'
        WHEN 24 THEN '/Content/images/products/watch6.jpg'
        WHEN 25 THEN '/Content/images/products/watch7.jpg'
        WHEN 26 THEN '/Content/images/products/watch8.jpg'
        WHEN 27 THEN '/Content/images/products/watch9.jpg'
        WHEN 28 THEN '/Content/images/products/watch10.jpg'
        WHEN 29 THEN '/Content/images/products/watch11.jpg'
        WHEN 30 THEN '/Content/images/products/watch12.jpg'
    END as NewPath
FROM productimages
ORDER BY ProductId;

-- =============================================
-- OPTION 2: INDIVIDUAL UPDATES FOR EACH PRODUCT
-- Update one product at a time
-- =============================================

-- Product 1 - Rolex Submariner Date -> watch1.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch1.jpg' WHERE ProductId = 1;

-- Product 2 - Omega Speedmaster Professional -> watch2.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch2.jpg' WHERE ProductId = 2;

-- Product 3 - TAG Heuer Carrera Calibre 16 -> watch3.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch3.jpg' WHERE ProductId = 3;

-- Product 4 - Rolex Datejust 36 -> watch4.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch4.jpg' WHERE ProductId = 4;

-- Product 5 - Omega Seamaster Aqua Terra -> watch5.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch5.jpg' WHERE ProductId = 5;

-- Product 6 - Seiko Prospex Diver 200m -> watch6.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch6.jpg' WHERE ProductId = 6;

-- Product 7 - Casio G-Shock Mudmaster -> watch7.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch7.jpg' WHERE ProductId = 7;

-- Product 8 - Citizen Promaster Diver -> watch8.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch8.jpg' WHERE ProductId = 8;

-- Product 9 - TAG Heuer Aquaracer Professional 300 -> watch9.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch9.jpg' WHERE ProductId = 9;

-- Product 10 - Seiko 5 Sports Automatic -> watch10.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch10.jpg' WHERE ProductId = 10;

-- Product 11 - Omega Constellation Manhattan -> watch11.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch11.jpg' WHERE ProductId = 11;

-- Product 12 - Longines DolceVita -> watch12.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch12.jpg' WHERE ProductId = 12;

-- Product 13 - Citizen Eco-Drive Silhouette -> watch13.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch13.jpg' WHERE ProductId = 13;

-- Product 14 - Tissot PRX Powermatic 80 -> watch14.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch14.jpg' WHERE ProductId = 14;

-- Product 15 - Tissot Gentleman Powermatic 80 -> watch15.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch15.jpg' WHERE ProductId = 15;

-- Product 16 - Hamilton Intra-Matic Auto Chrono -> watch16.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch16.jpg' WHERE ProductId = 16;

-- Product 17 - Orient Bambino Classic -> watch17.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch17.jpg' WHERE ProductId = 17;

-- Product 18 - Casio G-Shock GA-2100 -> watch18.jpg
UPDATE productimages SET ImageUrl = '/Content/images/products/watch18.jpg' WHERE ProductId = 18;

-- Product 19 - Rolex GMT-Master II -> watch1.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch1.jpg' WHERE ProductId = 19;

-- Product 20 - Omega Seamaster Diver 300M -> watch2.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch2.jpg' WHERE ProductId = 20;

-- Product 21 - TAG Heuer Monaco -> watch3.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch3.jpg' WHERE ProductId = 21;

-- Product 22 - Rolex Day-Date 40 -> watch4.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch4.jpg' WHERE ProductId = 22;

-- Product 23 - Omega De Ville Prestige -> watch5.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch5.jpg' WHERE ProductId = 23;

-- Product 24 - Seiko Presage Cocktail Time -> watch6.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch6.jpg' WHERE ProductId = 24;

-- Product 25 - Casio G-Shock Rangeman -> watch7.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch7.jpg' WHERE ProductId = 25;

-- Product 26 - Citizen Chronomaster Sport -> watch8.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch8.jpg' WHERE ProductId = 26;

-- Product 27 - TAG Heuer Formula 1 -> watch9.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch9.jpg' WHERE ProductId = 27;

-- Product 28 - Seiko Astron GPS Solar -> watch10.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch10.jpg' WHERE ProductId = 28;

-- Product 29 - Omega Speedmaster Moonwatch -> watch11.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch11.jpg' WHERE ProductId = 29;

-- Product 30 - Longines Master Collection -> watch12.jpg (reuse)
UPDATE productimages SET ImageUrl = '/Content/images/products/watch12.jpg' WHERE ProductId = 30;

-- =============================================
-- OPTION 3: BULK UPDATE USING CASE STATEMENT
-- Update all at once with one query
-- =============================================

UPDATE productimages
SET ImageUrl = CASE ProductId
    WHEN 1 THEN '/Content/images/products/watch1.jpg'
    WHEN 2 THEN '/Content/images/products/watch2.jpg'
    WHEN 3 THEN '/Content/images/products/watch3.jpg'
    WHEN 4 THEN '/Content/images/products/watch4.jpg'
    WHEN 5 THEN '/Content/images/products/watch5.jpg'
    WHEN 6 THEN '/Content/images/products/watch6.jpg'
    WHEN 7 THEN '/Content/images/products/watch7.jpg'
    WHEN 8 THEN '/Content/images/products/watch8.jpg'
    WHEN 9 THEN '/Content/images/products/watch9.jpg'
    WHEN 10 THEN '/Content/images/products/watch10.jpg'
    WHEN 11 THEN '/Content/images/products/watch11.jpg'
    WHEN 12 THEN '/Content/images/products/watch12.jpg'
    WHEN 13 THEN '/Content/images/products/watch13.jpg'
    WHEN 14 THEN '/Content/images/products/watch14.jpg'
    WHEN 15 THEN '/Content/images/products/watch15.jpg'
    WHEN 16 THEN '/Content/images/products/watch16.jpg'
    WHEN 17 THEN '/Content/images/products/watch17.jpg'
    WHEN 18 THEN '/Content/images/products/watch18.jpg'
    WHEN 19 THEN '/Content/images/products/watch1.jpg'
    WHEN 20 THEN '/Content/images/products/watch2.jpg'
    WHEN 21 THEN '/Content/images/products/watch3.jpg'
    WHEN 22 THEN '/Content/images/products/watch4.jpg'
    WHEN 23 THEN '/Content/images/products/watch5.jpg'
    WHEN 24 THEN '/Content/images/products/watch6.jpg'
    WHEN 25 THEN '/Content/images/products/watch7.jpg'
    WHEN 26 THEN '/Content/images/products/watch8.jpg'
    WHEN 27 THEN '/Content/images/products/watch9.jpg'
    WHEN 28 THEN '/Content/images/products/watch10.jpg'
    WHEN 29 THEN '/Content/images/products/watch11.jpg'
    WHEN 30 THEN '/Content/images/products/watch12.jpg'
    ELSE ImageUrl -- Keep existing if ProductId not matched
END
WHERE ProductId BETWEEN 1 AND 30;

-- =============================================
-- CUSTOM: UPDATE SPECIFIC PRODUCTS
-- Uncomment and modify as needed
-- =============================================

-- Example: Change Product 1 to use watch5.jpg instead
-- UPDATE productimages SET ImageUrl = '/Content/images/products/watch5.jpg' WHERE ProductId = 1;

-- Example: Change multiple products to same image
-- UPDATE productimages SET ImageUrl = '/Content/images/products/watch1.jpg' WHERE ProductId IN (1, 19, 22);

-- Example: Change based on product name (requires JOIN)
-- UPDATE productimages pi
-- JOIN products p ON pi.ProductId = p.ProductId
-- SET pi.ImageUrl = '/Content/images/products/watch10.jpg'
-- WHERE p.ProductName LIKE '%Rolex%';

-- =============================================
-- VERIFICATION
-- =============================================

-- View all product images with product names
SELECT 
    p.ProductId,
    p.ProductName,
    pi.ImageUrl,
    CASE 
        WHEN pi.ImageUrl LIKE '/Content/images/products/watch%.jpg' THEN '? Correct Format'
        ELSE '? Wrong Format'
    END as Status
FROM products p
LEFT JOIN productimages pi ON p.ProductId = pi.ProductId
WHERE pi.IsPrimary = 1
ORDER BY p.ProductId;

-- Count by status
SELECT 
    CASE 
        WHEN ImageUrl LIKE '/Content/images/products/watch%.jpg' THEN 'Correct'
        ELSE 'Needs Fix'
    END as Status,
    COUNT(*) as Count
FROM productimages
WHERE IsPrimary = 1
GROUP BY Status;

-- Show products missing images
SELECT 
    p.ProductId,
    p.ProductName,
    'NO IMAGE' as Issue
FROM products p
LEFT JOIN productimages pi ON p.ProductId = pi.ProductId
WHERE pi.ImageId IS NULL;

-- =============================================
-- ROLLBACK (if you need to undo changes)
-- =============================================

-- Create backup before making changes
-- CREATE TABLE productimages_backup AS SELECT * FROM productimages;

-- Restore from backup if needed
-- DELETE FROM productimages;
-- INSERT INTO productimages SELECT * FROM productimages_backup;

-- =============================================
-- NOTES
-- =============================================

/*
HOW TO USE THIS FILE:

1. PREVIEW FIRST (Recommended):
   Run the first SELECT query to see what changes will be made

2. UPDATE ALL AT ONCE:
   Run OPTION 3 (BULK UPDATE) - Updates all 30 products in one query

3. UPDATE ONE BY ONE:
   Run individual UPDATE statements from OPTION 2
   Good if you want to change specific products

4. CUSTOM UPDATES:
   Modify the CUSTOM section to make your own changes

5. VERIFY:
   Run VERIFICATION queries to check results

PRODUCT TO IMAGE MAPPING:
- Products 1-18: Use unique images (watch1.jpg through watch18.jpg)
- Products 19-30: Reuse images (watch1.jpg through watch12.jpg)

IMAGE FILES REQUIRED:
Make sure these files exist in: Omlajue_Ecommerce\Content\images\products\
- watch1.jpg through watch18.jpg

PATH FORMAT:
Always use: /Content/images/products/watchX.jpg
NOT: /images/products/ (missing /Content)
*/
