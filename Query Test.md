1. Primary & Foreign Key Tabel
    - TablePembayaran:  
        - Primary Key:  
            - NoKontrak  
        - Foreign Key:  
            - KodeCabang
            - KodeMotor

    - TabelCabang:  
        - Primary Key:  
            - KodeCabang  
        - Foreign Key: &dash;

    - TabelMotor:  
        - Primary Key:  
            - KodeMotor  
        - Foreign Key: &dash;

2. Query Data Pembayaran yang Dibayar pada 20-10-2014  
    ```sql
    SELECT *
    FROM TabelPembayaran
    WHERE CONVERT(DATE, TglBayar) = '2014-10-20';
    ```

3. Query Tambah Cabang  
    ```sql
    INSERT INTO TabelCabang(KodeCabang, NamaCabang)
    VALUES('200', 'Tangerang');
    ```

4. Query Update KodeMotor pada TabelPembayaran
    ```sql
    UPDATE TabelPembayaran
    SET KodeMotor = '001'
    WHERE KodeCabang IN (
        SELECT KodeCabang
        FROM TabelCabang
        WHERE NamaCabang = 'Jakarta'
    );
    ```

5. Query Menampilkan Data
    ```sql
    SELECT
        TP.NoKontrak AS NoKontrak,
        TP.TglBayar AS TglBayar,
        TP.JumlahBayar AS JumlahBayar,
        TC.KodeCabang AS KodeCabang,
        TC.NamaCabang AS NamaCabang,
        TP.NoKwitansi AS NoKwitansi,
        TM.KodeMotor AS KodeMotor,
        TM.NamaMotor AS NamaMotor
    FROM TabelPembayaran AS TP
        JOIN TabelCabang AS TC ON TC.KodeCabang = TP.KodeCabang
        JOIN TabelMotor AS TM ON TM.KodeMotor = TP.KodeMotor
    ```

6. Query Menampilkan Data 2
    ```sql
    SELECT
        TC.KodeCabang AS KodeCabang,
        TC.NamaCabang AS NamaCabang,
        TP.NoKontrak AS NoKontrak,
        TP.NoKwitansi AS NoKwitansi
    FROM TabelPembayaran AS TP
        JOIN TabelCabang AS TC ON TC.KodeCabang = TP.KodeCabang
    ORDER BY TC.KodeCabang ASC;
    ```

7. Query Menampilkan Data 3
    ```sql
    SELECT 
        TC.KodeCabang AS KodeCabang,
        TC.NamaCabang AS NamaCabang,
        COUNT(TP.NoKontrak) AS TotalData,
        COALESCE(SUM(TP.JumlahBayar), 0) AS TotalBayar
    FROM TabelCabang AS TC
        LEFT JOIN TabelPembayaran AS TP ON TC.KodeCabang = TP.KodeCabang
    GROUP BY TC.KodeCabang, TC.NamaCabang
    ORDER BY TC.KodeCabang ASC;
    ```