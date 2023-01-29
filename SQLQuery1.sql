/*UPDATE Pojazd
SET Image = 
(SELECT Bulkcolumn FROM Openrowset(Bulk 'C:\Users\tomaszkubicki\Documents\Zdjecia\Czerwony.png', Single_Blob) as Image)
WHERE Id = 3;*/
UPDATE Pojazd SET Silnik = 2.1 WHERE Id > 0;
