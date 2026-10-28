using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Matrix
{
    private int[,] _data;

    public int Rows
    {
        get { return _data.GetLength(0); }
    }

    public int Cols
    {
        get { return _data.GetLength(1); }
    }

    public Matrix(int rows, int cols)
    {
        _data = new int[rows, cols];
    }

    public Matrix(int[,] data)
    {
        _data = (int[,])data.Clone();
    }

    public int Get(int i, int j)
    {
        return _data[i, j];
    }

    public void Set(int i, int j, int value)
    {
        _data[i, j] = value;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                sb.Append(_data[i, j].ToString().PadLeft(5));
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
        {
            throw new ArgumentException("Размерности " +
                "матриц должны совпадать.");
        }
        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Cols; j++)
            {
                result.Set(i, j, a.Get(i, j) + b.Get(i, j));
            }
        }
        return result;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
        {
            throw new ArgumentException("Размерности " +
                "матриц должны совпадать.");
        }
        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Cols; j++)
            {
                result.Set(i, j, a.Get(i, j) - b.Get(i, j));
            }
        }
        return result;
    }

    public static Matrix operator *(int scalar, Matrix a)
    {
        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Cols; j++)
            {
                result.Set(i, j, a.Get(i, j) * scalar);
            }
        }
        return result;
    }

    public Matrix Transpose()
    {
        Matrix result = new Matrix(Cols, Rows);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                result.Set(j, i, Get(i, j));
            }
        }
        return result;
    }
}