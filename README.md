# PolWarmDictionary_Backend

So far Polish version only, I'm sorry.

Aplikacja nie jest jeszcze gotowym projektem!

ASP.Net Core Web API – słownik warmińsko-polski polsko-warmiński. Frontend do niniejszego słownika znajduje się w repozytorium: https://github.com/JakubJT/PolWarmDictionary_Frontend.

Użyte technologie, rozwiązania:
- aplikacja backendowa w technologii ASP.Net Core Web API,
- zastosowany wzorzec CQRS,
- zastosowany wzorzec mediator – przy użyciu biblioteki MediatR,
- biblioteka AutoMapper – do łatwego mapowania obiektów między sobą,
- Autentykacja/Autoryzacja przez Azure Active Directory,
- obsługa błędów przez ErrorControler,
- logowanie błędów do kontenera Blob na Azurze.


Funkcjonalności w rozbiciu na poszczególne kontrolery

WordController – kontroler do obsługi zapytań dotyczących słów ze słownika.
- Endpointy GET:

  - /word/getwords – endpoint wykorzystywany do pobierania z bazy danych określonego zestawu słów ze wszystkich słów w bazie danych. Przyjmuje argumenty określające jak słowa mają być posortowane (string sortBy), czy mają być ułożone w porządku alfabetycznym czy na odwrót (bool ascendingOrder), na jak duże zestawy mają być podzielone wszystkie słowa (int wordsPerPage) i który z kolei zestaw słów zwrócić (int pageNumber).
  Zwraca obiekt zawierający listę słów oraz ile zestawów słów (stron) znajduje się w bazie danych przy uwzględnieniu ile słów może się znaleźć w jednym zestawie (stronie).
  - /word/getword – endpoint wykorzystywany do wyszukiwania tłumaczeń danego słowa z warmińskiego lub polskiego. Przyjmuje argumenty zawierające samo słowo (string word) oraz wskazanie czy słowo ma być tłumaczone z polskiego czy też nie (bool translateFromPolish; jeśli nie to tłumaczone jest z warmińskiego).
  Zwraca listę słów, które stanowią tłumaczenie przekazanego słowa.
  - /word/getwordbyid – endpoint wykorzystywany do pobierania danego słowa z bazy danych przy wykorzystaniu id tego słowa. Jako argument przyjmuje id słowa (int wordId). Zwraca poszukiwane słowo (obiekt Word).

- Endpointy POST:

  - /word/wordcreateword – endpoint służący do utworzenia w bazie danych nowego słowa. Jako argument przyjmuje obiekt Word (Word word).
  - /word/editword – endpoint służący do modyfikacji w bazie danych danego słowa. Jako argument przyjmuje obiekt Word (Word word).

- Endpoint DELETE:

  - /word/deleteword – endpoint służący do usuwania danego słowa z bazy danych. Jako argument przyjmuje id słowa (int wordId).

WordGroupController – kontroler do obsługi zapytań dotyczących grup słów (kolekcji słów, które może tworzyć użytkownik).
- Endpointy GET:

  - /wordgroup/getallwordgroups – endpoint zwraca wszystkie grupy słów (listę obiektów WordGroup) utworzone przez danego użytkownika. Informacje o użytkowniku pozyskiwane są przy pomocy zaimplementowanej w programie autentykacji/autoryzacji.
  - /wordgroup/getwordgroup – endpoint służący do pobierania określonej grupy słów. Jako argument wykorzystywane jest id grupy (int wordGroupId).

- Endpointy POST:

  - /wordgroup/createwordgroup – endpoint służący do utworzenia w bazie danych nowej grupy słów.  Jako argument przyjmuje grupę słów (WordGroup wordGroup).
  - /wordgroup/editwordgroup – endpoint służący do edytowania w bazie danych grupy słów.  Jako argument przyjmuje grupę słów (WordGroup wordGroup).
    
- Endpoint DELETE:

  - /wordgroup/deletewordgroup – endpoint służący do usuwania z bazy danych określonej grupy słów. Jako argument przyjmuje id grupy, która ma zostać usunięta (int wordGroupId).
