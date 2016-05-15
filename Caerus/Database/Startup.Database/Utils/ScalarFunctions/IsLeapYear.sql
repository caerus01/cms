CREATE function [dbo].[IsLeapYear] (@year int)
returns bit
as
begin
    return(select case datepart(mm, dateadd(dd, 1, cast((cast(@year as varchar(4)) + '0228') as datetime))) 
    when 2 then 1 
    else 0 
    end)
end