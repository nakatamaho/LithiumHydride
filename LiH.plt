set encoding utf8
set terminal pdfcairo color enhanced font "Helvetica,18"
set xrange [0.5:3]
set yrange [-8.0:-7.0]
#plot "data_LiH_G" using 1:2 smooth csplines linewidth 4, \
#     "data_LiH_E1" using 1:2 smooth csplines linewidth 4, \
#     "data_LiH_E2" using 1:2 smooth csplines linewidth 4, \
#     "data_LiH_E3" using 1:2 smooth csplines linewidth 4, \
#     "data_LiH_E4" using 1:2 smooth csplines linewidth 4, \
#     "data_LiH_E5" using 1:2 smooth csplines linewidth 4
plot "data_LiH_G" using 1:2, \
     "data_LiH_E1" using 1:2, \
     "data_LiH_E2" using 1:2, \
     "data_LiH_E3" using 1:2, \
     "data_LiH_E4" using 1:2, \
     "data_LiH_E5" using 1:2
