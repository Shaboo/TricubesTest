namespace TricubesTest
{
    public class MatrixCell
    {
        char letter;
        public char Letter
        {
            get
            {
                return letter;
            }

            set
            {
                letter = value;
            }
        }

        bool found;
        public bool Found
        {
            get
            {
                return found;
            }

            set
            {
                found = value;
            }
        }

        public MatrixCell(char letter)
        {
            this.Letter = letter;
            this.Found = false;
        }
    }
}
