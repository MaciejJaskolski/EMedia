# PROJEKT NA EMEDIA
### ODKODOWANIE PLIKU WAV

Program napisany w c# + wpf, dodatkowo uzyto bibliotek:
*  AForge.Math - transformata fouriera

Punktem wejsciowym programu jest plik MainWindow.xaml.cs, ktory zawiera widok okna 
oraz wszystkie funkcje sluzace do operacji na pliku wav. 

### WAVHEADER.cs, WAVDATA.cs
Pliki te odpowiednio zawieraja definicje metadanych naglowka wav oraz jego danych
(ograniczonych do pierwszych 1024 sampli).

### WAVReader.cs
Klasa ta  odpowiada za czytanie danych z pliku .wav

### WAVWriter.cs 
Klasa ta zapisuje dane do pliku .wav

### Cipher.cs, Decipher.cs
Odpowiednio szyfrowanie i deszyfrowanie pliku

### RSA.cs
Klasa ta szyfruje dane algorytmem RSA

### DFT.cs 
Klasa ta generuje punkty wykresy na podstawie FFT