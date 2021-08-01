Contoh penggunaan Hangfire sebagai background job. Server hangfire dan client-nya dipisah menjadi 2 aplikasi berbeda.

HangfireDashboard - Aplikasi web yang digunakan untuk menampilkan aplikasi web dan juga dashboard dari hangfire.
					Aplikasi ini juga yang melakukan pendaftaran request job untuk di proses oleh background job.

BackgroundAppService	- Service layer (dll) yang memiliki fitur register job ke hangfire dan business logic untuk
						  memproses job dari backgrounder.

HangfireServerNetCore	- Console/Service application (topshelf) yang akan menjadi server hangfire dan memproses
						  background job yang sudah didaftarkan oleh client.

Cara Menjalankannya:
1. Buat database dengan nama hangfiredb di server SQL Server.
2. Ubah connection string pada appsettings.json di project HangfireDashboard dan HangfireServerNetCore
3. Build aplikasi HangfireServerNetCore.
4. Jalankan aplikasi web dengan menekan F5, buka tab baru untuk membuka dashboard hangfire https://localhost:44360/hangfire
5. Buat request tiket baru dengan menjalankan swagger (tab default yang terbuka ketika run F5)
6. Perhatikan dashboard, server masih (0) sehingga job tidak di proses.
7. Buka console, masuk ke folder HangfireServerNetCore/Bin/Debug/netcore5/ jalankan HangfireServerNetCore.exe
8. Perhatikan dashboard, server menjadi (1) kemudian job bisa di proses.