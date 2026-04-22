create table kategori (
	id serial primary key,
	nama varchar(100),
	crated_at timestamp default current_timestamp,
	updated_at timestamp default current_timestamp
);

create table suppliers(
	id serial primary key,
	nama varchar(100),
	telepon varchar(100),
	alamat text,
	created_at timestamp default current_timestamp,
	updated_at timestamp default current_timestamp
);

create table produk(
	id serial primary key,
	nama varchar(100),
	harga decimal(10,2),
	stok int,
	kategori_id int,
	supplier_id int,
	created_at timestamp default current_timestamp,
	updated_at timestamp default current_timestamp,
	foreign key (kategori_id) references kategori(id),
	foreign key (supplier_id) references suppliers(id)
);

create table transaksi(
	id serial primary key,
	total_harga decimal(10,2),
	transaksi_tanggal timestamp default current_timestamp,
	created_at timestamp default current_timestamp,
	updated_at timestamp default current_timestamp
);

create table detail_transaksi(
	id serial primary key,
	transaksi_id int,
	produk_id int,
	jumlah int,
	subtotal decimal(10,2),
	created_at timestamp default current_timestamp,
	updated_at timestamp default current_timestamp,
	foreign key (transaksi_id) references transaksi(id),
	foreign key (produk_id) references produk(id)
);

INSERT INTO kategori (nama) VALUES
('ATK'), 
('Makanan'), 
('Minuman'), 
('Elektronik'), 
('Lainnya');

INSERT INTO suppliers (nama, telepon, alamat) VALUES
('PT Sumber Jaya', '081234567890', 'Jl. Merdeka No.1, Bandung, Jawa Barat'),
('CV Maju Jaya', '082345678901', 'Jl. Sudirman No.10, Jakarta, DKI Jakarta'),
('UD Makmur', '083456789012', 'Jl. Diponegoro No.5, Surabaya, Jawa Timur'),
('PT Sejahtera', '084567890123', 'Jl. Gatot Subroto No.8, Medan, Sumatera Utara'),
('CV Sentosa', '085678901234', 'Jl. Ahmad Yani No.3, Semarang, Jawa Tengah');

INSERT INTO produk (nama, harga, stok, kategori_id, supplier_id) VALUES
('Pensil', 2000, 100, 1, 1),
('Pulpen', 3000, 80, 1, 2),
('Roti', 5000, 50, 2, 3),
('Air Mineral', 3000, 70, 3, 4),
('Mouse', 50000, 20, 4, 5);

INSERT INTO transaksi (total_harga) VALUES
(10000),
(15000),
(20000),
(5000),
(12000);

INSERT INTO detail_transaksi (transaksi_id, produk_id, jumlah, subtotal) VALUES
(1, 1, 2, 4000),
(1, 2, 2, 6000),
(2, 3, 3, 15000),
(3, 5, 1, 20000),
(4, 4, 1, 5000);