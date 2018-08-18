# Sklep internetowy _CinaMonn_

Sklep internetowy _CinaMonn_ jest aplikacj¹, za pomoca której mo¿na dokonaæ zakupu ubrañ, obuwia i akcesoriów z dzia³u damskiego. 

Technologie u¿yte przy implementacji aplikacji:
* ASP .NET 4.6.1
* MVC 5.2.4
* Entity Framework 6.2.0 Code First
* Bootstrap 3.3.7
* jQuery 3.3.1

Obecnie aplikacja umo¿liwia dokonywanie operacji CRUD na produktach, ich rozmiarach i kategoriach.  
Oprócz podstawowych ju¿ zdefiniowanych kategorii istnieje mo¿liwoœæ dodania w³asnych. Ponadto ka¿da kategoria posiada specyficzne dla siebie rozmiary. Mo¿liwe jest tak¿e dodawanie w³asnych produktów wraz z ich zdjêciami. Ka¿da z tych akcji wymaga zalogowania na konto administratora lub managera sklepu.
Na pasku g³ównym  w zale¿noœci od zdefiniowanych ról wyœwietlane s¹ nastêpuj¹ce zak³adki:
*  _Produkty_ dla wszystkich u¿ytkowników
*  _Produkty_ , _Kategorie produktów_ , _Rozmiary_  dla zalogowanych administratorów (dla zak³adki _Produkty_ widok  jest inny ni¿ dla u¿ytkownika)
* wszystkie zak³adki administratora oraz zak³adka _Administracja_ dla zalogowanego managera sklepu, który oprócz podstawowych operacji na produktach mo¿e tak¿e dodawaæ i usuwaæ administratorów. 


### U¿ytkownicy
W aplikacji bêdzie mo¿na wyró¿niæ trzy rodzaje u¿ytkowników oraz przyk³adowe konta: 
* User (user@cinamonn.pl)
* Admin (monika@cinamonn.pl)
* Manager (shopmanager@cinamonn.pl)

Dla ka¿dego z tych kont mo¿na zalogowaæ siê przy u¿yciu has³a: Zaq1@wsx. Mo¿liwe jest równie¿ za³o¿enie w³asnego konta przy u¿yciu dowolnie wybranej domeny.

Rola u¿ytkownika (_User_) przypisana jest do ka¿dego u¿ytkownika zak³adaj¹cego konto w aplikacji.
Rola administratora (_Admin_) przypisywana jest u¿ytkownikowi wy³¹cznie przez managera sklepu, który jest jedynym tego typu u¿ytkownikiem . Administratorem mo¿e zostaæ tylko pracownik sklepu tzn. musi posiadaæ konto w domenie _cinamonn.pl_.
Rola managera (_Manager_) dodawana jest do domyœlnego u¿ytkownika, który tworzony jest w momencie startu aplikacji.
Ka¿dy z powy¿szych typów u¿ytkowników posiada mo¿liwoœæ zmiany has³a.

### Planowany rozwój aplikacji:
* podkategorie produktów
* filtry do przeszukiwania produktów 
* dodawanie produktów do koszyka, zwiêkszanie ich ilosci oraz usuwanie z koszyka
* przejscie do zamówienia, w którym dostêpny jest podglad dodanych produktów oraz mo¿liwosæ edycji swoich danych osobowych do wysy³ki
* mo¿liwosæ edycji adresu, który jest póŸniej automatycznie pobierany do danych w zamówieniu
* podglad dokonanych zamówieñ.

### Informacje dodatkowe
W celach demonstracyjnych za³¹czona zosta³a przyk³adowa baza danych sklepu.

Wszystkie zdjêcia znajduj¹ce siê w aplikacji pochodz¹ ze strony https://www.bershka.com i zosta³y do³¹czone tylko w celach prezentacji projektu. 