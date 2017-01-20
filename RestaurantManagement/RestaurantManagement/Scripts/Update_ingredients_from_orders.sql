CREATE TRIGGER Update_Ingredients_From_Orders
ON Ordering_dishes
AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	DECLARE @Ingredient_Name nvarchar(30), @Quantity Integer;

	DECLARE Contents CURSOR
		LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR 
		SELECT Ingredient_Name, Quantity
		FROM Dish_contents
		WHERE Dish_ID = COALESCE(inserted.Dish_ID,deleted.Dish_ID);
	OPEN Contents;
	FETCH NEXT FROM Contents
	INTO @Ingredient_Name, @Quantity;

	WHILE @@FETCH_STATUS = 0  
	BEGIN
		UPDATE 
			Ingredients
		SET 
			Quantity_in_stock = CASE WHEN inserted.Dish_ID IS NULL THEN Quantity_in_stock+@Quantity*deleted.Quantity
								 WHEN deleted.Dish_ID IS NULL THEN Quantity_in_stock+inserted.Quantity*@Quantity
								 ELSE Quantity_in_stock+deleted.Quantity*@Quantity-inserted.Quantity*@Quantity
								 END
		FROM
			Ingredients
		INNER JOIN
		(
			inserted
		FULL OUTER JOIN
			deleted
				ON inserted.Dish_ID = deleted.Dish_ID
		)
		ON Ingredients.Name=@Ingredient_Name;
	FETCH NEXT FROM Contents
	INTO @Ingredient_Name, @Quantity;
	END
END