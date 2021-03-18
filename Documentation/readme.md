# Documentation

1 Discuss the assignment (and document this in the documentation-file), so you have a common picture of what you are building.
 * 

2 Discuss (and document this in the documentation-file) the user experience (UX) the users should have when using the console application for the parking system:
  * What should be in the screen
    *  Meny Switch/Case
    *  Användar registrering / inloggning (Checkas mot databas)
    *  Skriva in rymdskeppets namn som skall checkas mot databasen (Exklusivt skepp?)
    *  Parkera
    *  Avbryta parkering när man åker
    *  Betalning sker med faktura. (JSON?)
  * Language of the application (Klingonska)
  * How to interact with the application? mouse, keyboard, other (Keyboard)
  * Is there a menu? JA! Which options should it have: Se ovan!

3 Discuss (and document this in the documentation-file) which data the system generate, and try to draw an ER-diagram of the data
  Nedan är våra klasser! där vissa kanske har Interfaces.
  * SpaceCustomer
    * ID
  * SpaceShip
    * ID
  * SpacePort(Parkerings hus) 
    * ID 
  * Parkeringsvakt kollar av om namnet finns i databasen och rymdskeppet. (Dörrvakt)
    * Metod som tar in det externa API't och kollar av i lista om godkänt.
    * Metod som ger en parkingsplats och välkommnar kunden eller be honom dra till Vulcan.
    * Metod som lägger in godkänd data från API't i databasen.
  * SpaceSlot(parkeringsplatser) (Antal platser per station, inklusive special parkering)
    * ID
    * CustomerOccupant (Vem står på vilken parkeringsplats)
  * API En klass som mappar informationen från kunden. (Gästlista)
  * Klass som lägger in godkänd data från API't i databasen.
  * Ekonomiavdelning som skickar fakturan

4 Open the given solution-file in Visual Studio and add a new console application

5 Add a unit test project to the solution file

6 Implement a very simple flow of a scenario and unit tests which can confirm that it works, the flow could consist of:
  * The user entering his name
  * Making a simple request against the Starwars Web API to all persons
  * Check the name the user entered against the persons from the API request
  * Creating a simple table in the database using Entity Framework
  * Store in the database that the user have registered himself
  * List in the UI all registrations which have been done so far

7 Extend the application functionality and a very simple menu (if you have decided to make a menu).

