Feature: Simple failure Example
	As a start for the ratcheter 
	I want to be able to send in all relevant numbers on the commandline 
	and see the result written to the output


Scenario: Current value is higher than Target
	Given that the parameter is set to towardsZero
	And Current input is higher than Target
	When I run the application
	Then it logs to ouput as failure
