# ClientsAPI

This is a Client API with those characteristics:
* Responsible to handle new clients registration
* It is independent and should be called from a backend that controls all Rest API calls
* It has features related to clients creation, update and search using the following requirements:
** Client identification will be a CPF.
** CPF must be valid.
** Client is allowed to have only one address.
** Client is allowed to have multiple phone numbers.
** Client has a Name, Email and Marital Status.
** All data is mandatory, system will prevent registrations with missing information.

IMPORTANT: In order to work properly, it is necessary to inform a valid path to persist data at Test\App.config on the StorageFilePath Key. This folder needs to have access to Read and Write.

The following practices were consistently used during the project:
* TDD
* SOLID
* Repository Pattern

Fernando Lopes
fernando.cv.lopes@gmail.com
