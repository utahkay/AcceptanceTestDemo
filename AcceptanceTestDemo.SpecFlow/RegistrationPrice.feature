Feature: RegistrationPrice
	In order to decide whether to attend the conference
	As a potential attendee
	I want to know my registration price

@mytag
Scenario: Single registration
	Given I am purchasing 1 registration for Agile Austria
	Then the total price should be 500

Scenario: Group registration
    Given I am purchasing 5 registrations for Agile Austria
	Then the unit price should be 450
	And the total price should be 2250

Scenario: Discount code
    Given I am purchasing 1 registration for Agile Austria
	And I provide coupon code HALFOFF
	Then the total price should be 250

Scenario: Late registration
    Given I am purchasing 1 registration for Agile Austria
	And there are 6 days until the conference
	Then the total price should be 700



