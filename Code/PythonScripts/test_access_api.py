import requests

response = requests.get("http://localhost:5501/gesamttubenAnz/Machine1/1")
print(response.json())

#goals variables

goal_a1 = requests.get("http://localhost:5501/gesamttubenanzZiel/4Machines").json()
global_goal = goal_a1 * 4

#print all goals
print(goal_a1)
print(global_goal)

#amount variables
amount_a1 = requests.get("http://localhost:5501/gesamttubenAnz/Machine1/1").json()
amount_a2 = requests.get("http://localhost:5501/gesamttubenAnz/Machine1/2").json()
amount_a3 = requests.get("http://localhost:5501/gesamttubenAnz/Machine2/1").json()
amount_a4 = requests.get("http://localhost:5501/gesamttubenAnz/Machine2/2").json()

#print all amounts
print(amount_a1)
print(amount_a2)
print(amount_a3)
print(amount_a4)

error_a1 = requests.get("http://localhost:5501/kaputteTubenAnz/TAA1").json()
error_a2 = requests.get("http://localhost:5501/kaputteTubenAnz/TAA2").json()
error_a3 = requests.get("http://localhost:5501/kaputteTubenAnz/TAA3").json()
error_a4 = requests.get("http://localhost:5501/kaputteTubenAnz/TAA4").json()

#print all errors
print("Errors:")
print(error_a1)
print(error_a2)
print(error_a3)
print(error_a4)
