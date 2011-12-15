Feature: Simple Warning Example
	As a start for the ratcheter 
	I want to be able to send in all relevant numbers on the commandline 
	and see the result written to the output



Scenario: Current value is lower than Target but within warning
	Given that the parameter is set to towardsZero
	And Current input is lower than Target
	But Current plus warning is higher than Target
	When I run the application
	Then it logs to output as warning

