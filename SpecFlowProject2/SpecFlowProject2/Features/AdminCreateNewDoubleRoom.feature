Feature: AdminCreateNewDoubleRoom

As an admin user, I want to read create new double room and update description and image 


Scenario Outline: Create new room with amenities and update description and image
	Given the user has logged into the admin section with "<username>" and "<password>"
	And  the admin page is opened
	When the new room with "<type>" and amenities "<roomDetails1>" "<roomDetails2>" "<roomDetails3>" is created
	#Then the new room with "<type>" and amenities "<roomDetails1>" "<roomDetails2>" "<roomDetails3>" is displayed on admin page
	When selecting the room 
	Then edit button is displayed on page
	When enter edit mode
	Then description field is displayed
	And  image field is displayed
	Then update "<description>" and "<image>"
	When clicking update button
	Then description room should be updated with "<description>" 
	And image room with should be updated "<image>" 

Examples: 
	| username | password | type   | roomDetails1 | roomDetails2 | roomDetails3 | description                     | image                                          |
	| admin | password | Double | Refreshments | TV           | Safe         | New double room with splendid view | https://media.xmlcal.com/pic/p0002/0697/32.png |