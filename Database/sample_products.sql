-- =============================================
-- OMLAJUE E-COMMERCE - EXPANDED SAMPLE DATA
-- Comprehensive product catalog with ALL watch images
-- =============================================

USE omlajue_ecommerce;

-- Clear existing data (in correct order to respect foreign keys)
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE orderitems;
TRUNCATE TABLE payments;
TRUNCATE TABLE orders;
TRUNCATE TABLE wishlistitems;
TRUNCATE TABLE cartitems;
TRUNCATE TABLE productimages;
TRUNCATE TABLE products;
TRUNCATE TABLE categories;
TRUNCATE TABLE brands;
TRUNCATE TABLE users;
SET FOREIGN_KEY_CHECKS = 1;

-- =============================================
-- INSERT BRANDS
-- =============================================
INSERT INTO brands (BrandName, Description, LogoUrl, IsActive) VALUES
('Rolex', 'Swiss luxury watch manufacturer known for precision and prestige', '/images/brands/rolex.png', 1),
('Omega', 'Swiss luxury watchmaker famous for Speedmaster and Seamaster collections', '/images/brands/omega.png', 1),
('TAG Heuer', 'Swiss luxury watch brand known for sports watches and chronographs', '/images/brands/tagheuer.png', 1),
('Seiko', 'Japanese watch manufacturer offering quality timepieces at various price points', '/images/brands/seiko.png', 1),
('Casio', 'Japanese electronics company famous for G-Shock and digital watches', '/images/brands/casio.png', 1),
('Citizen', 'Japanese watch manufacturer known for Eco-Drive technology', '/images/brands/citizen.png', 1),
('Tissot', 'Swiss watchmaker offering accessible luxury timepieces', '/images/brands/tissot.png', 1),
('Longines', 'Swiss luxury watch company with elegant designs', '/images/brands/longines.png', 1),
('Hamilton', 'American-Swiss watch brand known for innovative designs', '/images/brands/hamilton.png', 1),
('Orient', 'Japanese watch manufacturer specializing in mechanical watches', '/images/brands/orient.png', 1),
('Patek Philippe', 'Swiss luxury watch manufacturer with exceptional heritage', '/images/brands/patek.png', 1),
('Audemars Piguet', 'Swiss manufacturer of luxury mechanical watches', '/images/brands/ap.png', 1);

-- =============================================
-- INSERT CATEGORIES
-- =============================================
INSERT INTO categories (CategoryName, Description, ImageUrl, IsActive, DisplayOrder) VALUES
('Luxury Watches', 'Premium timepieces from world-renowned brands', '/images/categories/luxury.jpg', 1, 1),
('Sports Watches', 'Durable watches built for active lifestyles and athletics', '/images/categories/sports.jpg', 1, 2),
('Dress Watches', 'Elegant timepieces perfect for formal occasions', '/images/categories/dress.jpg', 1, 3),
('Dive Watches', 'Water-resistant watches designed for underwater activities', '/images/categories/dive.jpg', 1, 4),
('Chronographs', 'Multi-function watches with stopwatch capabilities', '/images/categories/chrono.jpg', 1, 5),
('Smart Watches', 'Connected timepieces with digital features', '/images/categories/smart.jpg', 1, 6),
('Vintage Collection', 'Classic designs with timeless appeal', '/images/categories/vintage.jpg', 1, 7),
('Limited Editions', 'Exclusive and rare timepiece collections', '/images/categories/limited.jpg', 1, 8);

-- =============================================
-- INSERT PRODUCTS - Using ALL 18 Watch Images
-- =============================================
INSERT INTO products (ProductName, SKU, Description, Specifications, Price, DiscountPrice, StockQuantity, LowStockThreshold, 
    CaseMaterial, BandMaterial, MovementType, WaterResistance, CaseDiameter, Gender, IsFeatured, IsActive, CreatedAt, CategoryId, BrandId) 
VALUES
-- Product 1 - watch1.jpg
('Rolex Submariner Date', 'ROL-SUB-001', 
'The ultimate dive watch with iconic design and exceptional performance. Features a unidirectional rotating bezel and Cerachrom insert.',
'Movement: Perpetual, mechanical, self-winding | Calibre: 3235 | Power Reserve: ~70 hours | Waterproof: 300m/1000ft',
850000, NULL, 5, 2, 'Oystersteel', 'Oystersteel', 'Automatic', '300m', '41mm', 'Men', 1, 1, NOW(), 4, 1),

-- Product 2 - watch2.jpg
('Omega Speedmaster Professional', 'OMG-SPD-001',
'The legendary Moonwatch worn during historic space missions. Manual-wind chronograph with exceptional precision.',
'Movement: Manual-wind | Calibre: 1861 | Chronograph: Yes | Crystal: Hesalite | Tachymeter: Yes',
520000, 475000, 8, 2, 'Stainless Steel', 'Stainless Steel Bracelet', 'Manual', '50m', '42mm', 'Men', 1, 1, NOW(), 5, 2),

-- Product 3 - watch3.jpg
('TAG Heuer Carrera Calibre 16', 'TAG-CAR-001',
'Racing-inspired chronograph with sleek design. Perfect blend of sportiness and elegance.',
'Movement: Automatic chronograph | Calibre: 16 | Power Reserve: 42 hours | Functions: Date, Chronograph',
295000, 265000, 12, 3, 'Stainless Steel', 'Leather Strap', 'Automatic', '100m', '43mm', 'Men', 1, 1, NOW(), 5, 3),

-- Product 4 - watch4.jpg
('Rolex Datejust 36', 'ROL-DJ-002',
'Timeless elegance with the iconic Cyclops lens. The quintessential dress watch for any occasion.',
'Movement: Perpetual, self-winding | Calibre: 3235 | Power Reserve: 70 hours | Bracelet: Jubilee',
680000, NULL, 6, 2, '18ct Yellow Gold & Steel', 'Jubilee', 'Automatic', '100m', '36mm', 'Unisex', 1, 1, NOW(), 3, 1),

-- Product 5 - watch5.jpg
('Omega Seamaster Aqua Terra', 'OMG-SEA-002',
'Sophisticated sports-elegant watch with Co-Axial movement. Ideal for both business and leisure.',
'Movement: Co-Axial Master Chronometer | Calibre: 8900 | Anti-magnetic: 15,000 Gauss | Accuracy: 0/+5 sec/day',
420000, 395000, 10, 3, 'Stainless Steel', 'Stainless Steel', 'Automatic', '150m', '41mm', 'Men', 1, 1, NOW(), 2, 2),

-- Product 6 - watch6.jpg
('Seiko Prospex Diver 200m', 'SEI-PRO-001',
'Professional dive watch with superior luminosity and reliability. ISO certified for diving.',
'Movement: Automatic 4R36 | Power Reserve: 41 hours | Lume: LumiBrite | ISO 6425 Certified',
28500, 25650, 25, 5, 'Stainless Steel', 'Rubber Strap', 'Automatic', '200m', '42mm', 'Men', 1, 1, NOW(), 4, 4),

-- Product 7 - watch7.jpg
('Casio G-Shock Mudmaster', 'CAS-GSH-001',
'Virtually indestructible watch built for extreme conditions. Mud and shock resistant.',
'Movement: Quartz | Functions: World Time, Stopwatch, Timer, Alarm | Solar Powered | Tough Solar',
18900, 16900, 35, 8, 'Carbon Core Guard', 'Resin', 'Quartz', '200m', '56mm', 'Men', 1, 1, NOW(), 2, 5),

-- Product 8 - watch8.jpg
('Citizen Promaster Diver', 'CIT-PRO-001',
'Eco-Drive diver powered by light. Never needs a battery replacement.',
'Movement: Eco-Drive E168 | Power Reserve: 6 months | ISO 6425 | Depth Rating: 200m',
32000, 28800, 20, 5, 'Super Titanium', 'Polyurethane', 'Eco-Drive', '200m', '44mm', 'Men', 1, 1, NOW(), 4, 6),

-- Product 9 - watch9.jpg
('TAG Heuer Aquaracer Professional 300', 'TAG-AQA-001',
'Modern dive watch with unidirectional rotating bezel and exceptional water resistance.',
'Movement: Calibre 5 Automatic | Power Reserve: 38 hours | Bezel: Ceramic | Helium Escape Valve',
185000, NULL, 15, 4, 'Stainless Steel', 'Stainless Steel', 'Automatic', '300m', '43mm', 'Men', 1, 1, NOW(), 4, 3),

-- Product 10 - watch10.jpg
('Seiko 5 Sports Automatic', 'SEI-5SP-001',
'Affordable automatic watch with day-date display. Perfect entry-level mechanical watch.',
'Movement: 4R36 Automatic | Power Reserve: 41 hours | Hacking: Yes | Hand-winding: Yes',
12500, 10900, 40, 10, 'Stainless Steel', 'NATO Strap', 'Automatic', '100m', '42mm', 'Unisex', 1, 1, NOW(), 2, 4),

-- Product 11 - watch11.jpg
('Omega Constellation Manhattan', 'OMG-CON-001',
'Elegant ladies watch with iconic "claws" and diamond markers. Symbol of luxury and precision.',
'Movement: Co-Axial Master Chronometer | Calibre: 8700 | Diamonds: 12 | Mother of Pearl Dial',
480000, 445000, 8, 2, '18ct Sedna Gold & Steel', 'Steel/Gold Bracelet', 'Automatic', '50m', '29mm', 'Women', 1, 1, NOW(), 1, 2),

-- Product 12 - watch12.jpg
('Longines DolceVita', 'LON-DOL-001',
'Rectangular Art Deco inspired timepiece. Sophisticated elegance for the modern woman.',
'Movement: Quartz | Case Shape: Rectangular | Dial: Silver with applied numerals',
85000, 76500, 15, 4, 'Stainless Steel', 'Leather Strap', 'Quartz', '30m', '23mm x 37mm', 'Women', 1, 1, NOW(), 3, 8),

-- Product 13 - watch13.jpg
('Citizen Eco-Drive Silhouette', 'CIT-SIL-001',
'Ultra-thin ladies watch powered by light. Minimalist design with maximum elegance.',
'Movement: Eco-Drive E031 | Thickness: 6.8mm | Power Reserve: 12 months | Crystals: Swarovski',
22000, NULL, 20, 5, 'Stainless Steel', 'Stainless Steel', 'Eco-Drive', '50m', '28mm', 'Women', 1, 1, NOW(), 3, 6),

-- Product 14 - watch14.jpg
('Tissot PRX Powermatic 80', 'TIS-PRX-001',
'Retro-inspired integrated bracelet watch. Vintage charm with modern movement.',
'Movement: Powermatic 80 | Power Reserve: 80 hours | See-through caseback | Waffle dial',
38000, 34200, 18, 4, 'Stainless Steel', 'Integrated Bracelet', 'Automatic', '100m', '35mm', 'Women', 1, 1, NOW(), 7, 7),

-- Product 15 - watch15.jpg
('Tissot Gentleman Powermatic 80', 'TIS-GEN-001',
'Modern dress watch with 80-hour power reserve. Understated elegance for the discerning gentleman.',
'Movement: Powermatic 80 Silicium | Power Reserve: 80 hours | Dial: Sunray blue | COSC Chronometer',
45000, NULL, 12, 3, 'Stainless Steel', 'Leather Strap', 'Automatic', '50m', '40mm', 'Men', 1, 1, NOW(), 3, 7),

-- Product 16 - watch16.jpg
('Hamilton Intra-Matic Auto Chrono', 'HAM-INT-001',
'Vintage-inspired chronograph with modern reliability. American spirit meets Swiss precision.',
'Movement: H-31 Automatic | Power Reserve: 60 hours | Chronograph: Bi-compax | Panda dial',
120000, 108000, 10, 3, 'Stainless Steel', 'Leather Strap', 'Automatic', '50m', '40mm', 'Men', 1, 1, NOW(), 5, 9),

-- Product 17 - watch17.jpg
('Orient Bambino Classic', 'ORI-BAM-001',
'Affordable dress watch with in-house automatic movement. Exceptional value proposition.',
'Movement: F6724 Automatic | Power Reserve: 40 hours | Dome crystal | Hand-winding',
9800, 8820, 30, 8, 'Stainless Steel', 'Leather Strap', 'Automatic', '30m', '40.5mm', 'Men', 1, 1, NOW(), 3, 10),

-- Product 18 - watch18.jpg
('Casio G-Shock GA-2100', 'CAS-GSH-002',
'The "CasiOak" - Octagonal bezel design inspired by luxury sports watches. Slim profile for G-Shock.',
'Movement: Quartz | Shock Resistant | Functions: World Time, Stopwatch | Carbon Core Guard',
7500, 6750, 50, 12, 'Carbon Reinforced Resin', 'Resin', 'Quartz', '200m', '45mm', 'Unisex', 1, 1, NOW(), 2, 5);

-- =============================================
-- INSERT PRODUCT IMAGES - All watch images mapped
-- =============================================
INSERT INTO productimages (ProductId, ImageUrl, IsPrimary, DisplayOrder) VALUES
-- Product 1 - watch1.jpg
(1, '/Content/images/products/watch1.jpg', 1, 1),
-- Product 2 - watch2.jpg
(2, '/Content/images/products/watch2.jpg', 1, 1),
-- Product 3 - watch3.jpg
(3, '/Content/images/products/watch3.jpg', 1, 1),
-- Product 4 - watch4.jpg
(4, '/Content/images/products/watch4.jpg', 1, 1),
-- Product 5 - watch5.jpg
(5, '/Content/images/products/watch5.jpg', 1, 1),
-- Product 6 - watch6.jpg
(6, '/Content/images/products/watch6.jpg', 1, 1),
-- Product 7 - watch7.jpg
(7, '/Content/images/products/watch7.jpg', 1, 1),
-- Product 8 - watch8.jpg
(8, '/Content/images/products/watch8.jpg', 1, 1),
-- Product 9 - watch9.jpg
(9, '/Content/images/products/watch9.jpg', 1, 1),
-- Product 10 - watch10.jpg
(10, '/Content/images/products/watch10.jpg', 1, 1),
-- Product 11 - watch11.jpg
(11, '/Content/images/products/watch11.jpg', 1, 1),
-- Product 12 - watch12.jpg
(12, '/Content/images/products/watch12.jpg', 1, 1),
-- Product 13 - watch13.jpg
(13, '/Content/images/products/watch13.jpg', 1, 1),
-- Product 14 - watch14.jpg
(14, '/Content/images/products/watch14.jpg', 1, 1),
-- Product 15 - watch15.jpg
(15, '/Content/images/products/watch15.jpg', 1, 1),
-- Product 16 - watch16.jpg
(16, '/Content/images/products/watch16.jpg', 1, 1),
-- Product 17 - watch17.jpg
(17, '/Content/images/products/watch17.jpg', 1, 1),
-- Product 18 - watch18.jpg
(18, '/Content/images/products/watch18.jpg', 1, 1);

-- =============================================
-- INSERT DEMO USERS
-- =============================================
INSERT INTO users (FirstName, LastName, Email, PasswordHash, PhoneNumber, Address, City, Province, PostalCode, IsAdmin, IsActive, CreatedAt) VALUES
('John', 'Doe', 'john.doe@example.com', 'hashed_password_here', '09171234567', '123 Main Street', 'Manila', 'Metro Manila', '1000', 0, 1, NOW()),
('Jane', 'Smith', 'jane.smith@example.com', 'hashed_password_here', '09181234567', '456 Oak Avenue', 'Quezon City', 'Metro Manila', '1100', 0, 1, NOW()),
('Admin', 'User', 'admin@omlajue.com', 'hashed_password_here', '09191234567', '789 Admin St', 'Makati', 'Metro Manila', '1200', 1, 1, NOW());

-- =============================================
-- VERIFICATION QUERIES
-- =============================================
SELECT 'Brands Created:' as Info, COUNT(*) as Count FROM brands;
SELECT 'Categories Created:' as Info, COUNT(*) as Count FROM categories;
SELECT 'Products Created:' as Info, COUNT(*) as Count FROM products;
SELECT 'Product Images Created:' as Info, COUNT(*) as Count FROM productimages;
SELECT 'Users Created:' as Info, COUNT(*) as Count FROM users;

SELECT 
    b.BrandName,
    COUNT(p.ProductId) as ProductCount,
    MIN(p.Price) as MinPrice,
    MAX(p.Price) as MaxPrice
FROM brands b
LEFT JOIN products p ON b.BrandId = p.BrandId
GROUP BY b.BrandId, b.BrandName
ORDER BY ProductCount DESC;
