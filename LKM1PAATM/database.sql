
//create table kategori (
//	id serial primary key,
//    nama varchar(100),
//    crated_at timestamp default current_timestamp,
//    updated_at timestamp default current_timestamp
//);

//create table suppliers(
//	id serial primary key,
//    nama varchar(100),
//    telepon varchar(100),
//    alamat text,
//    created_at timestamp default current_timestamp,
//    updated_at timestamp default current_timestamp
//);

//create table produk(
//	id serial primary key,
//    nama varchar(100),
//    harga decimal(10,2),
//    stok int,
//    kategori_id int,
//    supplier_id int,
//    created_at timestamp default current_timestamp,
//    updated_at timestamp default current_timestamp,
//    foreign key (kategori_id) references kategori(id),
//    foreign key (supplier_id) references suppliers(id)
//);

//create table transaksi(
//	id serial primary key,
//    total_harga decimal(10,2),
//    transaksi_tanggal timestamp default current_timestamp,
//    created_at timestamp default current_timestamp,
//    updated_at timestamp default current_timestamp
//);

//create table detail_transaksi(
//	id serial primary key,
//    transaksi_id int,
//    produk_id int,
//    jumlah int,
//    subtotal decimal(10,2),
//    created_at timestamp default current_timestamp,
//    updated_at timestamp default current_timestamp,
//    foreign key (transaksi_id) references transaksi(id),
//    foreign key (produk_id) references produk(id)
//);

//create index idx_produk_kategori on produk(kategori_id);
//create index idx_produk_supplier on produk(supplier_id);
//create index idx_transaksi_detail_transaksi on detail_transaksi(transaksi_id);
//create index idx_detail_transaksi_produk on detail_transaksi(produk_id);

//insert into kategori (nama) values
//('Makanan'),
//('Minuman'),
//('Snack'),
//('Elektronik'),
//('ATK');

//insert into suppliers (nama, telepon, alamat) values
//('PT Multi Niaga Sejahtera', '081256839022', 'Jl. Merdeka No. 45, Bandung, Jawa Barat'),
//('Global Stationery Supply', '085608260278', 'Jl. Sudirman Kav. 21, Jakarta Selatan, DKI Jakarta'),
//('ElectroMart Distribusi Nusantara', '086723659144', 'Jl. Tunjungan No. 88, Surabaya, Jawa Timur'),
//('Fresh & Pack Logistics', '081167835290', 'Jl. Ijen No. 12, Malang, Jawa Timur'),
//('Mega Retail Partners', '086389402811', 'Jl. Raya Seminyak No. 33, Badung, Bali');

//insert into produk (nama, harga, stok, kategori_id, supplier_id) values
//('Sedaap Goreng', 3500, 100, 1, 1),
//('Teh Botol', 5000, 80, 2, 2),
//('Choco Wallens', 10000, 50, 3, 3),
//('Mouse Logitech', 75000, 20, 4, 4),
//('Pulpen Joyko', 3000, 200, 5, 5);

//insert into transaksi (total_harga) values
//(7000),
//(10000),
//(15000),
//(20000),
//(5000);

//insert into detail_transaksi (transaksi_id, produk_id, jumlah, subtotal) VALUES
//(1, 1, 2, 7000),
//(2, 2, 2, 10000),
//(3, 3, 1, 10000),
//(4, 4, 1, 75000),
//(5, 5, 2, 6000);

//select* from produk
//select * from kategori
//select * from transaksi
//select * from suppliers
//select * from detail_transaksi