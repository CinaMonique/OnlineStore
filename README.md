# Sklep internetowy _CinaMonn_

Sklep internetowy _CinaMonn_ jest aplikacj�, za pomoca kt�rej mo�na dokona� zakupu ubra�, obuwia i akcesori�w z dzia�u damskiego. 

Technologie u�yte przy implementacji aplikacji:
* ASP .NET 4.6.1
* MVC 5.2.4
* Entity Framework 6.2.0 Code First
* Bootstrap 3.3.7
* jQuery 3.3.1

Obecnie aplikacja umo�liwia dokonywanie operacji CRUD na produktach, ich rozmiarach i kategoriach.  Na pasku g��wnym dodano dwie podstawowe zak�adki: _Produkty-admin_ oraz _Produkty-user_. Maj� one charakter pogl�dowy pokazuj�cy mo�liwo�ci administratora aplikacji oraz jej niezalogowanego u�ytkownika. 
Po dodaniu r�l, kt�re b�d� kolejnym krokiem w kierunku rozwoju aplikacji - b�dzie widoczny tylko widok produkt�w dla u�ytkownika. Za� administrator po zalogowaniu si� b�dzie mia� dost�p do innego widoku (obecnie pod zak�adk� _Produkty-admin_) ni� zwyk�y u�ytkownik - widok ogranicza si� do funkcji, kt�re sa wpisane w rol� administratora.
Opr��z podstawowych ju� zdefiniowanych kategorii istnieje mo�liwo�� dodania w�asnych. Ponadto ka�da kategoria posiada specyficzne dla siebie rozmiary dodawane przez administratora. Mo�liwe jest tak�e dodawanie w�asnych produkt�w.  


### U�ytkownicy
W aplikacji b�dzie mo�na wyr�ni� dwa rodzaje u�ytkownik�w: administrator oraz klient sklepu.  Po zaimplementowaniu r�l ka�da z nich posiada� b�dzie specyficzne dla siebie akcje.

###Planowany rozw�j aplikacji:
* podkategorie produkt�w
* filtry do przeszukiwania produkt�w 
* dodawanie produkt�w do koszyka, zwi�kszanie ich ilosci oraz usuwanie z koszyka
* przejscie do zam�wienia, w kt�rym dost�pny jest podglad dodanych produkt�w oraz mo�liwos� edycji swoich danych osobowych do wysy�ki
* mo�liwos� edycji adresu, kt�ry jest p�niej automatycznie pobierany do danych w zam�wieniu
* podglad dokonanych zam�wie�.

### Informacje dodatkowe
W celach demonstracyjnych za��czona zosta�a przyk�adowa baza danych sklepu.

Wszystkie zdj�cia znajduj�ce si� w aplikacji pochodz� ze strony https://www.bershka.com i zosta�y do��czone tylko w celach prezentacji projektu. 