function ValidateTeacher() {

	// DOM elements
	const validationError = document.getElementById("validationError");
	const teacherFname = document.getElementById("TeacherFname").value;
	const teacherLname = document.getElementById("TeacherLname").value;
	const employeeNumber = document.getElementById("EmployeeNumber").value;
	const hireDate = document.getElementById("HireDate").value;
	const salary = document.getElementById("Salary").value;

	// Validation variables
	let isValid = true;
	let errorMsg = "";

	// Common function for checking required fields
	const requiredCheck = fieldName => (!fieldName || (fieldName && fieldName.trim().length === 0))

	// Validation on fields
	if (requiredCheck(teacherFname)) {
		isValid = false;
		errorMsg += "Teacher's first name is required.<br>"
	}
	if (requiredCheck(teacherLname)) {
		isValid = false;
		errorMsg += "Teacher's last name is required.<br>"
	}
	if (requiredCheck(employeeNumber)) {
		isValid = false;
		errorMsg += "Teacher's employee number is required.<br>"
	}
	if (requiredCheck(hireDate)) {
		isValid = false;
		errorMsg += "Teacher's hire date is required.<br>"
	}
	if (requiredCheck(salary)) {
		isValid = false;
		errorMsg += "Teacher's salary is required.<br>"
	}

	// Show error messages, when isValid is false else don't show error message
	if (!isValid) {
		validationError.style.display = "block";
		validationError.innerHTML = errorMsg;
	} else {
		validationError.style.display = "none";
		validationError.innerHTML = "";
	}

	return isValid;
}