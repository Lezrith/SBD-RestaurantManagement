CREATE TRIGGER Update_Ingredients_From_Orders
ON Dishes_contents
AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	UPDATE 
		Ingredients
	SET 
		Quantity_in_stock = CASE WHEN inserted.Quantity IS NULL THEN Quantity_in_stock+deleted.Quantity
								 WHEN deleted.Quantity IS NULL THEN Quantity_in_stock-inserted.Quantity
								 ELSE Quantity_in_stock+deleted.Quantity-inserted.Quantity
								 END
	FROM
		Ingredients
	INNER JOIN
	(
		inserted
	FULL OUTER JOIN
		deleted
			ON inserted.Ingredient_Name = deleted.Ingredient_Name
	)
	ON Ingredients.Name=COALESCE(inserted.Ingredient_Name,deleted.Ingredient_Name)
END