if exists(select top 1 * from Business where Businessid={0} and FrozenNo='')
begin
insert into deduct.HandDeductSequence
(SequenceGuid,Businessid,Amount,DeductAmount,
Balance,AccountNo,AccountName,IdentityCard,
AccountBank,ToAccount,BillItems,HasLitigation,
Createtime,Updatetime,OperationUserId,FrozenNo,
Step,Status)
values
('{13}',{0},{1},{2},{3},'{4}','{5}','{6}','{7}',
    '{8}','{9}','{10}',getdate(),getdate(),{11},'{12}',
    1,0)
update Business Set FrozenNo='{12}' where BusinessID ={0}
end