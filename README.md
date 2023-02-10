# PolWarmDictionary_Backend

English verion below.

Aplikacja nie jest jeszcze gotowym projektem!

ASP.Net Core Web API – słownik warmińsko-polski polsko-warmiński. Frontend do niniejszego słownika znajduje się w repozytorium: https://github.com/JakubJT/PolWarmDictionary_Frontend.

Użyte technologie, rozwiązania:
- aplikacja backendowa w technologii ASP.Net Core Web API,
- zastosowany wzorzec CQRS,
- zastosowany wzorzec mediator – przy użyciu biblioteki MediatR,
- biblioteka AutoMapper – do łatwego mapowania obiektów między sobą,
- Autentykacja/Autoryzacja przez Azure Active Directory,
- Microsoft Graph używany do pobierania informacji z Azure Active Directory,
- obsługa błędów przez ErrorControler,
- logowanie błędów do kontenera Blob na Azurze.


Funkcjonalności w rozbiciu na poszczególne kontrolery

WordController – kontroler do obsługi żądań dotyczących słów ze słownika.
- Endpointy GET:

  - /word/getwords – endpoint wykorzystywany do pobierania z bazy danych określonego zestawu słów. Przyjmuje argumenty określające jak słowa mają być posortowane (string sortBy), czy mają być ułożone w porządku alfabetycznym czy na odwrót (bool ascendingOrder), na jak duże zestawy mają być podzielone wszystkie słowa (int wordsPerPage) i który z kolei zestaw słów zwrócić (int pageNumber).
  Zwraca obiekt zawierający listę słów oraz liczbę, która wskazuje ile zestawów słów (stron) znajduje się w bazie danych przy uwzględnieniu ile słów może się znaleźć w jednym zestawie (stronie).
  - /word/getword – endpoint wykorzystywany do wyszukiwania tłumaczeń danego słowa z warmińskiego lub polskiego. Przyjmuje argumenty zawierające samo słowo (string word) oraz wskazanie czy słowo ma być tłumaczone z polskiego czy też nie (bool translateFromPolish; jeśli nie to tłumaczone jest z warmińskiego).
  Zwraca listę słów, które stanowią tłumaczenie przekazanego słowa.
  - /word/getwordbyid – endpoint wykorzystywany do pobierania danego słowa z bazy danych przy wykorzystaniu id tego słowa. Jako argument przyjmuje id słowa (int wordId). Zwraca poszukiwane słowo (obiekt Word).

- Endpointy POST:

  - /word/createword – endpoint służący do utworzenia w bazie danych nowego słowa. Jako argument przyjmuje obiekt Word (Word word).
  - /word/editword – endpoint służący do modyfikacji w bazie danych danego słowa. Jako argument przyjmuje obiekt Word (Word word).

- Endpoint DELETE:

  - /word/deleteword – endpoint służący do usuwania danego słowa z bazy danych. Jako argument przyjmuje id słowa (int wordId).

WordGroupController – kontroler do obsługi żądań dotyczących grup słów (kolekcji słów, które może tworzyć użytkownik).
- Endpointy GET:

  - /wordgroup/getallwordgroups – endpoint zwraca wszystkie grupy słów (listę obiektów WordGroup) utworzone przez użytkownika, który wysłał zapytanie. Informacje o użytkowniku pozyskiwane są przy pomocy zaimplementowanej w programie autentykacji/autoryzacji.
  - /wordgroup/getwordgroup – endpoint służący do pobierania określonej grupy słów. Jako argument wykorzystywane jest id grupy (int wordGroupId).

- Endpointy POST:

  - /wordgroup/createwordgroup – endpoint służący do utworzenia w bazie danych nowej grupy słów. Jako argument przyjmuje grupę słów (WordGroup wordGroup).
  - /wordgroup/editwordgroup – endpoint służący do edytowania w bazie danych grupy słów. Jako argument przyjmuje grupę słów (WordGroup wordGroup).
    
- Endpoint DELETE:

  - /wordgroup/deletewordgroup – endpoint służący do usuwania z bazy danych określonej grupy słów. Jako argument przyjmuje id grupy, która ma zostać usunięta (int wordGroupId).

UserController - kontroler do obsługi żądań dotyczących użytkowników.
- Endpointy GET:

	- /user/getallusers - endpoint zwraca wszystkich użytkowników z bazy danych wraz z informacją o liczbie posiadanych przez nich grup słów (zwraca listę obiektów User).


English version

The app is not finished yet!

ASP.Net Core Web API - Warmian-Polish/Polish-Warmian dictionary (Warmian is a subdialect of Polish). The frontend for this dictionary is in the repository: https://github.com/JakubJT/PolWarmDictionary_Frontend.

Technologies and solutions used:
- backend application in ASP.Net Core Web API technology,
- CQRS pattern used,
- mediator pattern used (app uses MediatR library),
- AutoMapper library - for easy mapping of objects with each other,
- Authentication/Authorization by Azure Active Directory,
- app uses Microsoft Graph to retrieve data from Azure Active Directory,
- error handling by ErrorControler,
- logging errors to Blob container on Azure.


Controllers functionalities

WordController – a controller for handling requests concerning words in dictionary.
- GET endpoints:

   - /word/getwords – endpoint used to retrieve a specific set of words from the database. It accepts arguments specifying how words need to be sorted (string sortBy), whether they should be arranged in alphabetical order or vice versa (bool ascendingOrder), how many words should be in one set (int wordsPerPage) and which set of words to return (int pageNumber).
   Returns an object containing a list of words and number that indicates how many sets of words (pages) are in the database, taking into account how many words can be found in one set (page).
   - /word/getword – endpoint used to search for translations of a given word from Warmian or Polish. It accepts arguments containing the word itself (string word) and an indication whether the word should be translated from Polish or not (bool translateFromPolish; if not, it is translated from Warmian).
   Returns a list of words that are translations of the given word.
   - /word/getwordbyid – endpoint used to retrieve a given word from the database using a word id. It takes a word id (int wordId) as an argument. Returns the searched word (Word object).

- POST endpoints:

   - /word/createword – endpoint for creating a new word in the database. It takes a Word object as an argument.
   - /word/editword – endpoint for modifying a given word in the database. It takes a Word object as an argument.

- Endpoint DELETE:

   - /word/deleteword – endpoint for deleting a given word from the database. It takes a word id (int wordId) as an argument.

WordGroupController – a controller for handling requests concnerning word groups (a collection of words that app user can create).
- GET endpoints:

   - /wordgroup/getallwordgroups – endpoint returns all word groups (a list of WordGroup objects) created by a requesting user. Information about the user is obtained using the authentication/authorization implemented in the program.
   - /wordgroup/getwordgroup – endpoint for retrieving a specific group of words. A group id (int wordGroupId) is used as an argument.

- POST endpoints:

   - /wordgroup/createwordgroup – endpoint for creating a new group of words in the database. It takes a group of words as an argument (WordGroup wordGroup).
   - /wordgroup/editwordgroup – endpoint for editing a group of words in the database. It takes a group of words as an argument (WordGroup wordGroup).
    
- Endpoint DELETE:

   - /wordgroup/deletewordgroup – endpoint for deleting a specific group of words from the database. It takes a word group id (int wordGroupId) as an argument

UserController - a controller to handle requests concerning users.
- GET endpoints:

  - /user/getallusers - endpoint returns all users from the database with information about the number of groups of words they have (returns a list of User objects).