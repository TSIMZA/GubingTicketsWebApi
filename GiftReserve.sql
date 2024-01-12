DROP TABLE dbo.ReservedGiftRegItem
GO

DROP PROCEDURE dbo.up_Reserve_Gift
GO

DROP PROCEDURE dbo.up_Get_Reserved_Gifts
GO


CREATE TABLE dbo.GiftRegItem (
  itemId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
  itemName VARCHAR(500) NOT NULL,
  itemImage VARCHAR(200) NOT NULL,
  itemCost DECIMAL(10,5)  NOT NULL,
  itemUrl  VARCHAR(500) NULL,
  itemStore VARCHAR(500) NULL,
  itemDescription  VARCHAR(500) NULL
)
GO

INSERT INTO [dbo].[GiftRegItem]
           ([itemImage]
			,[itemName]
           ,[itemCost]
           ,[itemUrl]
           ,[itemStore]
           ,[itemDescription])
     VALUES
('abc.PNG', 'Russell Hobbs Electric Frying Pan',899,
'https://www.game.co.za/game-za/en/All-Game-Categories/Appliances/Cooking/Electric-Fryers/Electric-Fry-Pans-%26-Woks/Russell-Hobbs-ELECTRIC-FRYING-PAN/p/806727-EA',
'GAME', NULL),

('abcd.PNG', 'Russell Hobbs Electric Pressure Cooker',1399,
'https://www.game.co.za/game-za/en/All-Game-Categories/Appliances/Cooking/Slow-Cookers%2C-Steamers-%26-Food-Warmers/Slow-Cookers-%26-Steamers/Russell-Hobbs-Electric-Pressure-Cooker-RHEP-7/p/734084-EA',
'GAME', NULL),

('abcde.PNG', 'Russell Hobbs 2400W Glide Pro Steam Iron',449,
'https://www.game.co.za/game-za/en/All-Game-Categories/Appliances/Kitchen-Appliances/Irons-%26-Accessories/Irons/Russell-Hobbs-2400W-Glide-Pro-Steam-Iron/p/801677-EA',
'GAME', NULL),

('abcdef.PNG', 'NESPRESSO Pixie Titan',2999,
'https://www.home.co.za/pdp/nespresso-pixie-titan-grey/_/A-154003AAAF3?gclid=Cj0KCQjw6s2IBhCnARIsAP8RfAiZZfhCGnrqLZOO6Duq1TfXOTSt_kAfI_rry3EBjW_AvFYtmGzwRHIaAjEyEALw_wcB&gclsrc=aw.ds',
'@home', NULL),

('abcdefg.PNG', 'Ciroa Honeycomb Baker Mini 4Pc',189,
'https://www.westpacklifestyle.co.za/product/honeycomb-baker-mini-4pc--92403',
'WESTPACK LIFESTYLE', NULL),

('abcdefgh.PNG', 'Bowl(s) 30.5X23x11',79.90,
'https://www.westpacklifestyle.co.za/product/bowl-30-5x23x11--90434',
'WESTPACK LIFESTYLE', NULL),

('abcdefghi.PNG', 'Ciroa Dip N Eat Bowl Round Red',94.90,
'https://www.westpacklifestyle.co.za/product/dip-n-eat-bowl-round-red--92402',
'WESTPACK LIFESTYLE', NULL),

('abcdefghij.PNG', 'Home Classix Round Salad Bowl',279.90,
'https://www.westpacklifestyle.co.za/product/round-salad-bowl-28x7-9cm--87431',
'WESTPACK LIFESTYLE', NULL),

('abcdefghijk.PNG', 'Viva Jaimi Porcelain Tea Pot 750Ml',449.90,
'https://www.westpacklifestyle.co.za/product/jaimi-porcelain-tea-pot-750ml--92181',
'WESTPACK LIFESTYLE', NULL)





CREATE TABLE dbo.ReservedGiftRegItem (
  itemId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
  reservedById  VARCHAR(500)  NOT NULL,
  reservedDateTime  DATETIME DEFAULT GETDATE()  NOT NULL
)
GO

CREATE VIEW dbo.vw_GiftRegItems
AS
	SELECT gi.*,
		   rgi.reservedById,
		   rgi.reservedDateTime,
		   IIF(rgi.itemId IS NULL, 0, 1) AS isReserved
	FROM dbo.GiftRegItem gi WITH (NOLOCK)
	LEFT JOIN dbo.ReservedGiftRegItem rgi  WITH (NOLOCK)
	ON gi.itemId = rgi.itemId
GO

CREATE PROCEDURE dbo.up_Reserve_Gift
  @itemId UNIQUEIDENTIFIER,
  @reservedById  VARCHAR(500)
AS

	INSERT INTO [dbo].[ReservedGiftRegItem]
           ([itemId]
           ,[reservedById]
           ,[reservedDateTime])
     VALUES (
				@itemId
			   ,@reservedById
			   ,GETDATE()
		  )

	SELECT * FROM  [dbo].[vw_GiftRegItems] gi
	ORDER BY gi.itemDescription, gi.itemStore, gi.itemCost
GO



CREATE PROCEDURE dbo.up_Get_All_Gifts
AS
	SELECT * FROM  [dbo].[vw_GiftRegItems] gi
	ORDER BY gi.itemDescription, gi.itemStore, gi.itemCost

GO