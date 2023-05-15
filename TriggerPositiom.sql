Create Trigger Delete_Position On POSITION After Delete AS
BEGIN
Declare @id int 
Select @id = ID From deleted
Delete from EMPLOYEE Where PositionID = @id
END