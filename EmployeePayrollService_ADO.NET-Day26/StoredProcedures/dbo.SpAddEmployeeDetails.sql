create procedure dbo.SpAddEmployeeDetails
	@EmpName	varchar(150),		
	@basic_pay float,		
	@StartDate	date,		
	@gender	char(1),	
	@phoneNumber	bigint,		
	 @department	varchar(150),
   @address varchar(150),
	@deductions	float,	
	@taxable_pay float,
	@income_tax	float,
	@net_pay	float
	as begin
	Insert into employee_payroll values(@EmpName,@basic_pay,@StartDate,@gender,@phoneNumber,@department,@address,@deductions,@taxable_pay,@income_tax,@net_pay)
	End