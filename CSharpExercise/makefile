build:
	docker-compose build
start:
	docker-compose up -d	
stop:
	docker-compose down
user_success:
	powershell "Invoke-RestMethod -Uri 'http://localhost:5000/user' -Headers @{Authorization=('Basic RVRhdW1lOiQyYSQxMSRPbEZ6eUo0MjVPNjdWU2Q1aWJERHJPZVhoRDFXVjJBNFA1aHJxV2R6ci94TEltRDd6TWVzRw==')}"
user_failed:
	powershell "Invoke-RestMethod -Uri 'http://localhost:5000/user' -Headers @{Authorization=('Basic RVRhdW1lOiQyYSQxMSRPbEZ6eUo0MjVPdWU2Q1aWJERHJPZVhoRDFXVjJBNFA1aHJxV2R6ci94TElt6TWVzRw==')}"
	