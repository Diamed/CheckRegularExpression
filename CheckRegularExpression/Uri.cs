namespace CheckRegularExpression
{
    internal class Uri
    {
		private string _address;
		public string Address
		{
			get	{ return _address; }
			private set
			{
				_address = value.Contains("http://") ? value : string.Concat("http://", value);
			}
		}

		public Uri(string address)
		{
			Address = address;
		}

		public override string ToString()
		{
			return Address;
		}
	}
}
