Feature: YoutubeSearchFeature
	In order to test search functionality on youtube
	As a developer
	I want to ensure the functionality is working end to end

@scopeBinding
Scenario: Youtube should search for the given keyword and should navigate to search
	Given I have navigated to the youtube website
	And I have entered London as search keyword
	When I press the search button
	Then I should be redirected to search results