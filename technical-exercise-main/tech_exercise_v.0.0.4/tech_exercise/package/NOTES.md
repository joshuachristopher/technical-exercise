# Architecture
Layered API with CQRS-style vertical slices with DAL nested in the BAL.

Considering the handlers inherit results from StargateAPI.Controllers.BaseResponse, so “business” depends on the API layer.

# The Plan
* Add unit test project
    * Add unit tests on most impactful methods
* Fix project structure
* Generate the database
* Enforce the rules
* Add UI
* Improve defensive coding
* Implement process logging

