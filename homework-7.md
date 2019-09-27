# Homework after class 7

## Make a better route handler for sql

In `SqlServerResponseGenerator`, the `IsInterested` method should handle the following routes correctly

- /sql/books (interested, `GeneralInfo` responder)
- /sql/books/ (interested, `GeneralInfo` responder)
- /sql/books/anything-else (interested, some other responder, even `Error`)
- /sql/books-anything-else (not interested, some other response generator)

Make unit tests that cover the cases.

## JSON serializer

Add a nice JSON serializer, and use it in `GeneralInfo` and `TableList` responders.

~~Don't forget to add unit tests **before** you change it, so that you can be sure that the change did not change the results.~~

## Server error handling

Somehow handle external errors (invalid connection string, server is offline etc) in `SqlServerResponseGenerator`

## Very hard bonus: 

Add all the code needed for our server to be able to handle upload files (i.e. file content in the request itself). Implement some kind of upload service that saves photos to a specified folder as a demo.