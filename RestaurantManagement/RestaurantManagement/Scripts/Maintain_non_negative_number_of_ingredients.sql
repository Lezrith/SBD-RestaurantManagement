ALTER TABLE Ingredients
ADD CONSTRAINT Maintain_non_negative_number_of_ingredients CHECK (Quantity_in_stock>=0);