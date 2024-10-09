public class Complex
{
    public float Real { get; set; }
    public float Imaginary { get; set; }

    // Constructeur
    public Complex(float real, float imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Surcharge de l'opérateur +
    public static Complex operator +(Complex c1, Complex c2)
    {
        return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
    }

    // Surcharge de l'opérateur -
    public static Complex operator -(Complex c1, Complex c2)
    {
        return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
    }

    // Surcharge de l'opérateur *
    public static Complex operator *(Complex c1, Complex c2)
    {
        return new Complex(
            c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
            c1.Real * c2.Imaginary + c1.Imaginary * c2.Real
        );
    }

    // Surcharge des opérateurs == et !=
    public static bool operator ==(Complex c1, Complex c2)
    {
        if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            return ReferenceEquals(c1, c2);

        return c1.Real == c2.Real && c1.Imaginary == c2.Imaginary;
    }

    public static bool operator !=(Complex c1, Complex c2)
    {
        return !(c1 == c2);
    }

    // Redéfinition de Equals pour assurer la cohérence avec les opérateurs ==
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Complex other = (Complex)obj;
        return this == other;
    }


    // Redéfinition de GetHashCode pour correspondre à l'implémentation de Equals
    public override int GetHashCode()
    {
        return Real.GetHashCode() ^ Imaginary.GetHashCode();
    }

    // Surcharge de ToString pour afficher un nombre complexe
    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }
}
