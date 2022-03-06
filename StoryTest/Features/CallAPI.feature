Feature: CallAPI
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](StoryTest/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: CallAPIAddNew
  Given I have the following request body:
  """
{
  "userName": "Micl",
  "firstName": "Leung",
  "lastName": "Michael",
  "address": "Kwong Chiu Terrace",
  "birthDate": "1961-03-04T00:00:00.000Z",
  "departmentId": 1
}
  """
  When I post this request to the "users" operation
  Then the result is a 200 ("OK") response
  And the response contains username ("Micl") and ID (1) and Department ("IT")
