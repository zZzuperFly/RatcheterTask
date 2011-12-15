Feature: Simple Example
	As a start for the ratcheter 
	I want to be able to send in all relevant numbers on the commandline 
	and see the result written to the output

Scenario: Current value is lower than Target
	Given that the parameter is set to towardsZero
	And Current input is lower than Target
	When I run the application
	Then it logs to output as success


