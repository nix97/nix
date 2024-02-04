#Created by Lukas Setiawan Feb,2 2024
#e-mail : lukassetiawan@yahoo.com
#Facebook : search>> Metode Numerik - Plus Programnya
#Other works on https://bitbucket.org/nixz97/nix/downloads/
#It's free to modify

from pymep.realParser import eval
import numpy as np
import matplotlib.pyplot as plt


again = "Y"
while again[0] in ("y", "Y"):
    print('Second-Order Ordinary Differential Equation Solver - using Runge-Kutta-Fehlberg method(RKF45)');
    print("It's an Initial value problem(IVP)")
    print("Interval [a,b]")
    print("Subinterval M")
    print()
    print("For example we have Y''(x) =G(x,y,z) where z=y' ")
    print("Y''(x) =G(x,y,z)=9*sin(5*x)-125*y-20*z")
    print("a=0")
    print("b=2")
    print("Y(x0)=0")
    print("Y'(x0)=0")
    print("M=100")
    print()

    print("For demo only,just input data above or input others")
    DiffEquation=input("Y''=")
    a=float(input("a="))
    b=float(input("b="))
    Yx0=float(input("Y(x0)="))
    Yx0_dash=float(input("Y'(x0)=")) #Y'(x0)
    M=int(input("M="))

    # This part is an example that no need to input at runtime(as comment only)
    #If want activate, just delete mark '''
    ''' 
    DiffEquation="9*sin(5*x)-125*y-20*z"
    a=0
    b=2
    Yx0=0
    Yx0_dash=0
    M=100
    '''
    h=(b-a)/M

    x = np.zeros(M + 2)  # initial value set to zero
    Yk= np.zeros(M+2)
    Yk_dash= np.zeros(M+2)

    Yk[0] = Yx0
    Yk_dash[0]=Yx0_dash

    for k in range(0,M+1):
        x[k] = a + k * h

        #RKF45 formula base on the book Numerical method for math,science & engineering by Jhon H. Matthew
        f1 = h*Yk_dash[k]

        # g1
        var = {"x": x[k], "y": Yk[k], "z": Yk_dash[k]}
        g1 = h*eval(DiffEquation, var)  # g1=G(x,y,z)

        f2 = h*(Yk_dash[k] + 0.25 * g1)

        # g2
        var = {"x": x[k] + 0.25 * h, "y": Yk[k] + 0.25 * f1, "z": Yk_dash[k] + 0.25 * g1}
        g2 = h*(eval(DiffEquation, var))

        f3 = h*(Yk_dash[k] +  3/32 * g1+9/32*g2)

        # g3
        var = {"x": x[k] + 3/8* h, "y": Yk[k] + 3/32* f1+9/32*f2, "z": Yk_dash[k] + 3/32 * g1+9/32*g2}
        g3 = h*eval(DiffEquation, var)

        f4 = h*(Yk_dash[k] + 1932/2197*g1-7200/2197*g2+7296/2197*g3)

        # g4
        var = {"x": x[k] +12/13* h, "y": Yk[k] + 1932/2197* f1-7200/2197*f2+7296/2197*f3,
               "z": Yk_dash[k] + 1932/2197*g1-7200/2197*g2+7296/2197*g3}
        g4 = h*(eval(DiffEquation, var))

        f5 = h * (Yk_dash[k] + 439/216 * g1 - 8 * g2 + 3680 / 513 * g3-845/4104*g4)

        # g5
        var = {"x": x[k] +h, "y": Yk[k] + 439/216 * f1 - 8 * f2 + 3680 / 513 * f3-845/4104*f4,
               "z": Yk_dash[k] + 439/216 * g1 - 8 * g2 + 3680 / 513 * g3-845/4104*g4}
        g5 = h * (eval(DiffEquation, var))

        f6 = h * (Yk_dash[k] - 8 / 27 * g1+ 2 * g2 -3544 / 2565 * g3 +1859 / 4104 * g4-11/40*g5)

        # g6
        var = {"x": x[k] +0.5* h, "y": Yk[k] - 8 / 27 * f1+ 2 * f2 -3544 / 2565 * f3 +1859 / 4104 * f4-11/40*f5,
               "z": Yk_dash[k] - 8 / 27 * g1+ 2 * g2 -3544 / 2565 * g3 +1859 / 4104 * g4-11/40*g5}

        g6 = h * (eval(DiffEquation, var))

        #other source on https://en.wikipedia.org/wiki/Runge%E2%80%93Kutta%E2%80%93Fehlberg_method
        #Formula from wiki
        #Six Slopes onh wiki not yet try
        #Yk[k + 1] = Yk[k] + 47/450 * f1 + 12/25 * f3 + 32/225 * f4 + 1/30 * f5+6/25*f6

        # Final Result
        #RKF45
        Yk[k+1] = Yk[k] + 25/216*f1 + 1408/2565 * f3 + 2197/4101* f4-1/5*f5
        Yk_dash[k + 1] = Yk_dash[k] + 25 / 216 * g1 + 1408 / 2565 * g3 + 2197 / 4101 * g4 - 1 / 5 * g5

        # RK5 Butcher
        #Yk[k+1] = Yk[k] + 16/135*f1 + 6656/12825 * f3 + 28561/56430* f4-9/50*f5+2/55*f6
        #Yk_dash[k + 1] = Yk_dash[k + 1] + 16 / 135 * g1 + 6656 / 12825 * g3 + 28561 / 56430 * g4 - 9/50*g5 +2/55 * g6

    # Display Final Result
    #Title
    print("{:<7} {:<16} {:<18} {:<35}".format('k','Xk','Yk',"Y'k"))
    print("==============================================================")
    for k in range(0,M+1):
       print ("{:<5} {:<10.4f} {:<20.15f} {:<25.15f}".format(k,x[k],Yk[k],Yk_dash[k]))

    print()

    #Graph
    for k in range(0, M + 1):
        plt.plot(x[k],Yk[k],'ro')
        plt.plot(x[k],Yk_dash[k],'bo')

    plt.title("Graph Second-Order Differential Equation")
    plt.xlabel("X k")
    plt.ylabel("Y k & Y ' k")
    plt.legend(["Y k", "Y ' k"], loc="upper right")
    plt.show()

    again = input("Another one (Y/N)?")







