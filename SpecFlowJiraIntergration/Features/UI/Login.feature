@Regression
@Login
Feature: Login Feature
    As an User
    I want to log in to website

    Scenario Outline: Login unsuccessfully
        Given I open Homepage
        And I navigate to Login page
        When I log in with "<email>" and "<password>"
        Then the "<message>" should display

        Examples:
            | Scenario                  | email        | password  | message                   |
            | No customer account found | Test@123.com | 12345678  | No customer account found |
            | Wrong email format        | Test         | 123456789 | Wrong email               |

    @Dictionary
    Scenario Outline: Login unsuccessfully - Dictionary
        Given I open Homepage
        And I navigate to Login page
        When I log in with following account
            | Field    | Value      |
            | Email    | <email>    |
            | Password | <password> |
        Then the "<message>" should display

        Examples:
            | Scenario           | email | password  | message     |
            | Wrong email format | Test  | 123456789 | Wrong email |

    @DataTable
    Scenario Outline: Login unsuccessfully - DataTable
        Given I open Homepage
        And I navigate to Login page
        When I log in with account below
            | Email   | Password   |
            | <email> | <password> |
        Then the "<message>" should display

        Examples:
            | Scenario                  | email        | password | message                   |
            | No customer account found | Test@123.com | 12345678 | No customer account found |
            | Wrong email format        | Test123      | 121616   | Wrong email               |