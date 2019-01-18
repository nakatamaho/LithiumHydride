rm -f data_LiH*
grep G log | awk '{print $6 " " $3}' | sed 's/;//g' > data_LiH_G
grep E1 log | awk '{print $6 " " $3}' | sed 's/;//g' > data_LiH_E1
grep E2 log | awk '{print $6 " " $3}' | sed 's/;//g' > data_LiH_E2
grep E3 log | awk '{print $6 " " $3}' | sed 's/;//g' > data_LiH_E3
grep E4 log | awk '{print $6 " " $3}' | sed 's/;//g' > data_LiH_E4
grep E5 log | awk '{print $6 " " $3}' | sed 's/;//g' > data_LiH_E5
gnuplot LiH.plt > LiH.pdf
