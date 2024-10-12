-- Create the ms_storage_location table
CREATE TABLE ms_storage_location (
    location_id VARCHAR(10) NOT NULL,
    location_name VARCHAR(100) NOT NULL,
    PRIMARY KEY (location_id)
);

-- Create the ms_user table
CREATE TABLE ms_user (
    user_id BIGINT NOT NULL IDENTITY(1,1),
    user_name VARCHAR(20) NOT NULL,
    password VARCHAR(50) NOT NULL,
    is_active BIT NOT NULL,
    PRIMARY KEY (user_id)
);

-- Create the tr_bpkb table
CREATE TABLE tr_bpkb (
    agreement_number VARCHAR(100) NOT NULL,
    bpkb_no VARCHAR(100) NOT NULL,
    branch_id VARCHAR(10) NOT NULL,
    bpkb_date DATETIME NULL,
    faktur_no VARCHAR(100) NOT NULL,
    faktur_date DATETIME NULL,
    location_id VARCHAR(10) NOT NULL,
    police_no VARCHAR(20) NOT NULL,
    bpkb_date_in DATETIME NULL,
    created_by VARCHAR(20) NOT NULL,
    created_on DATETIME NULL,
    last_updated_by VARCHAR(20) NOT NULL,
    last_updated_on DATETIME NULL,
    PRIMARY KEY (agreement_number),
    FOREIGN KEY (location_id) REFERENCES ms_storage_location(location_id) ON DELETE CASCADE
);

-- Create an index on the location_id column of the tr_bpkb table
CREATE INDEX IX_tr_bpkb_location_id ON tr_bpkb(location_id);
