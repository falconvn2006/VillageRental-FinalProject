CREATE TABLE customer (
    customer_id INT PRIMARY KEY,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    phone_number VARCHAR(14) NOT NULL, 
    email VARCHAR(100) NOT NULL,       
    is_banned TINYINT(1) NOT NULL
);

CREATE TABLE category_item (
    category_id INT PRIMARY KEY,
    description VARCHAR(200) NOT NULL
);

CREATE TABLE equipment (
    equipment_id INT PRIMARY KEY,
    category_id INT NOT NULL,
    name VARCHAR(50) NOT NULL,
    description VARCHAR(200) NOT NULL,
    daily_rental_cost DECIMAL(5, 2) NOT NULL,
    equipment_status VARCHAR(50) NOT NULL,
    available_quantity TINYINT NOT NULL,
    CONSTRAINT equipment_category_id_fk FOREIGN KEY (category_id) REFERENCES category_item(category_id)
);

CREATE TABLE rental_information (
    rental_id INT PRIMARY KEY,
    current_date_of_rental DATE NOT NULL,
    customer_id INT NOT NULL,
    rental_status VARCHAR(50) NOT NULL,
    CONSTRAINT rental_information_customer_id_fk FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
);

CREATE TABLE rental_item (
    rental_id INT,
    equipment_id INT,
    rental_date DATE NOT NULL,
    return_date DATE NOT NULL,
    quantity TINYINT NOT NULL,
    cost_of_rental DECIMAL(5, 2) NOT NULL,
    PRIMARY KEY (rental_id, equipment_id),
    CONSTRAINT rental_item_rental_id_fk FOREIGN KEY (rental_id) REFERENCES rental_information(rental_id),
    CONSTRAINT rental_item_equipment_id_fk FOREIGN KEY (equipment_id) REFERENCES equipment(equipment_id)
);

-- Insert category examples
INSERT INTO category_item VALUES (10, 'Power tools');
INSERT INTO category_item VALUES (20, 'Yard equipment');
INSERT INTO category_item VALUES (30, 'Compressors');
INSERT INTO category_item VALUES (40, 'Generators');
INSERT INTO category_item VALUES (50, 'Air tools');

-- Insert customer examples
INSERT INTO customer VALUES (1001, 'Doe', 'John', '(555) 555-1212', 'jd@sample.net', 0);
INSERT INTO customer VALUES (1002, 'Smith', 'Jane', '(555) 555-3434', 'js@live.com', 0);
INSERT INTO customer VALUES (1003, 'Lee', 'Michael', '(555) 555-5656', 'ml@sample.net', 0);

-- Insert equipment examples
INSERT INTO equipment VALUES (101, 10, 'Hammer drill', 'Powerful drill for concrete and masonry', 25.99, 'Good', 1);
INSERT INTO equipment VALUES (201, 20, 'Chainsaw', 'Gas-powered chainsaw for cutting wood', 49.99, 'Good', 1);
INSERT INTO equipment VALUES (202, 20, 'Lawn mower', 'Self-propelled lawn mower with mulching function', 19.99, 'Good', 1);
INSERT INTO equipment VALUES (301, 30, 'Small Compressor', '5 Gallon Compressor-Portable', 14.99, 'Good', 1);
INSERT INTO equipment VALUES (501, 50, 'Brad Nailer', 'Brad Nailer. Requires 3/4 to 1 1/2 Brad Nails', 10.99, 'Good', 1);

-- Insert rental_information examples
INSERT INTO rental_information VALUES (1000, '2024-02-15', 1001, 'Renting');
INSERT INTO rental_information VALUES (1001, '2024-02-16', 1002, 'Renting');

-- Insert rental_item examples
INSERT INTO rental_item VALUES (1000, 201, '2024-02-20', '2024-02-23', 1, 149.97);
INSERT INTO rental_item VALUES (1001, 501, '2024-02-21', '2024-02-25', 1, 43.96);
