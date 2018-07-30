# Sklep internetowy _CinaMonn_

Sklep internetowy _CinaMonn_ jest aplikacj¹, za pomoca której mo¿na dokonaæ zakupu ubrañ, obuwia i akcesoriów z dzia³u damskiego. 

Technologie u¿yte przy implementacji aplikacji:
* ASP .NET 4.6.1
* MVC 5.2.4
* Entity Framework 6.2.0 Code First
* Bootstrap 3.3.7
* jQuery 3.3.1

Obecnie aplikacja umo¿liwia dokonywanie operacji CRUD na produktach, ich rozmiarach i kategoriach.  Na pasku g³ównym dodano dwie podstawowe zak³adki: _Produkty-admin_ oraz _Produkty-user_. Maj¹ one charakter pogl¹dowy pokazuj¹cy mo¿liwoœci administratora aplikacji oraz jej niezalogowanego u¿ytkownika. 
Po dodaniu ról, które bêd¹ kolejnym krokiem w kierunku rozwoju aplikacji - bêdzie widoczny tylko widok produktów dla u¿ytkownika. Zaœ administrator po zalogowaniu siê bêdzie mia³ dostêp do innego widoku (obecnie pod zak³adk¹ _Produkty-admin_) ni¿ zwyk³y u¿ytkownik - widok ogranicza siê do funkcji, które sa wpisane w rolê administratora.
Opróæz podstawowych ju¿ zdefiniowanych kategorii istnieje mo¿liwoœæ dodania w³asnych. Ponadto ka¿da kategoria posiada specyficzne dla siebie rozmiary dodawane przez administratora. Mo¿liwe jest tak¿e dodawanie w³asnych produktów.  


### U¿ytkownicy
W aplikacji bêdzie mo¿na wyró¿niæ dwa rodzaje u¿ytkowników: administrator oraz klient sklepu.  Po zaimplementowaniu ról ka¿da z nich posiadaæ bêdzie specyficzne dla siebie akcje.

###Planowany rozwój aplikacji:
* podkategorie produktów
* filtry do przeszukiwania produktów 
* dodawanie produktów do koszyka, zwiêkszanie ich ilosci oraz usuwanie z koszyka
* przejscie do zamówienia, w którym dostêpny jest podglad dodanych produktów oraz mo¿liwosæ edycji swoich danych osobowych do wysy³ki
* mo¿liwosæ edycji adresu, który jest póŸniej automatycznie pobierany do danych w zamówieniu
* podglad dokonanych zamówieñ.

### Informacje dodatkowe
W celach demonstracyjnych za³¹czona zosta³a przyk³adowa baza danych sklepu.

Wszystkie zdjêcia znajduj¹ce siê w aplikacji pochodz¹ ze strony https://www.bershka.com i zosta³y do³¹czone tylko w celach prezentacji projektu. 