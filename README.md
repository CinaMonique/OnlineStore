# Sklep internetowy _CinaMonn_

Sklep internetowy _CinaMonn_ jest aplikacj�, za pomoca kt�rej mo�na dokona� zakupu ubra�, obuwia i akcesori�w z dzia�u damskiego. 

Technologie u�yte przy implementacji aplikacji:
* ASP .NET 4.6.1
* MVC 5.2.4
* Entity Framework 6.2.0 Code First
* Bootstrap 3.3.7
* jQuery 3.3.1

Obecnie aplikacja umo�liwia dokonywanie operacji CRUD na produktach, ich rozmiarach i kategoriach.  
Opr�cz podstawowych ju� zdefiniowanych kategorii istnieje mo�liwo�� dodania w�asnych. Ponadto ka�da kategoria posiada specyficzne dla siebie rozmiary. Mo�liwe jest tak�e dodawanie w�asnych produkt�w wraz z ich zdj�ciami. Ka�da z tych akcji wymaga zalogowania na konto administratora lub managera sklepu.
Na pasku g��wnym  w zale�no�ci od zdefiniowanych r�l wy�wietlane s� nast�puj�ce zak�adki:
*  _Produkty_ dla wszystkich u�ytkownik�w
*  _Produkty_ , _Kategorie produkt�w_ , _Rozmiary_  dla zalogowanych administrator�w (dla zak�adki _Produkty_ widok  jest inny ni� dla u�ytkownika)
* wszystkie zak�adki administratora oraz zak�adka _Administracja_ dla zalogowanego managera sklepu, kt�ry opr�cz podstawowych operacji na produktach mo�e tak�e dodawa� i usuwa� administrator�w. 


### U�ytkownicy
W aplikacji b�dzie mo�na wyr�ni� trzy rodzaje u�ytkownik�w oraz przyk�adowe konta: 
* User (user@cinamonn.pl)
* Admin (monika@cinamonn.pl)
* Manager (shopmanager@cinamonn.pl)

Dla ka�dego z tych kont mo�na zalogowa� si� przy u�yciu has�a: Zaq1@wsx. Mo�liwe jest r�wnie� za�o�enie w�asnego konta przy u�yciu dowolnie wybranej domeny.

Rola u�ytkownika (_User_) przypisana jest do ka�dego u�ytkownika zak�adaj�cego konto w aplikacji.
Rola administratora (_Admin_) przypisywana jest u�ytkownikowi wy��cznie przez managera sklepu, kt�ry jest jedynym tego typu u�ytkownikiem . Administratorem mo�e zosta� tylko pracownik sklepu tzn. musi posiada� konto w domenie _cinamonn.pl_.
Rola managera (_Manager_) dodawana jest do domy�lnego u�ytkownika, kt�ry tworzony jest w momencie startu aplikacji.
Ka�dy z powy�szych typ�w u�ytkownik�w posiada mo�liwo�� zmiany has�a.

### Planowany rozw�j aplikacji:
* podkategorie produkt�w
* filtry do przeszukiwania produkt�w 
* dodawanie produkt�w do koszyka, zwi�kszanie ich ilosci oraz usuwanie z koszyka
* przejscie do zam�wienia, w kt�rym dost�pny jest podglad dodanych produkt�w oraz mo�liwos� edycji swoich danych osobowych do wysy�ki
* mo�liwos� edycji adresu, kt�ry jest p�niej automatycznie pobierany do danych w zam�wieniu
* podglad dokonanych zam�wie�.

### Informacje dodatkowe
W celach demonstracyjnych za��czona zosta�a przyk�adowa baza danych sklepu.

Wszystkie zdj�cia znajduj�ce si� w aplikacji pochodz� ze strony https://www.bershka.com i zosta�y do��czone tylko w celach prezentacji projektu. 