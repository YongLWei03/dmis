cd c:\df8360\bin\backup\
del dbs1.dmp
del dbs2.dmp
exp df_dmis/df_dmis@dbs1 file="dbs1"
exp df_dmis/df_dmis@dbs1 file="dbs2"

