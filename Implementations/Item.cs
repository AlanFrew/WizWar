using System;
using System.Collections.Generic;

namespace WizWar1 {
	class Item : Locatable, IItem {
		public String Name { get; set; }

		protected Wizard carrier;
		public Wizard Carrier {
			get {
				return carrier;
			}
			set {
				if (carrier == value) {
					return;
				}

				if (value == null) {
					if (carrier != null) {
						carrier.loseItem(this);
					}

					//location = GameState.BoardReference.At(value.X, value.Y);
				}

				carrier = value;
				Controller = value;
			}
		}

		protected Square location;
		public Square Location {
			get {
				if (Carrier != null) {
					return GameState.BoardRef.At(Carrier.X, Carrier.Y);
				}

				return location;
			}
			set {
				if (location == value) {
					return;
				}

				if (carrier == null) {
					if (location != null) {
						location.RemoveItem(this);
					}
					value.AddLocatable(this);
					location = value;
					x = location.X;
					y = location.Y;
				}

				//can't use this property on a carried item; must be dropped() or lost() first
			}
		}

		public bool RequiresLoS { get; set; }

		public List<TargetTypes> ValidTargetTypes { get; set; }

		public Wizard Creator { get; set; }

		//public List<Effect> EffectsWaiting { get; set; }

		public ITarget ItemTarget { get; set; }

		public Item() {
			activeTargetType = TargetTypes.Item;

			ValidTargetTypes = new List<TargetTypes>();
			//EffectsWaiting = new List<Effect>();

			RequiresLoS = false;
		}

		public virtual bool IsValidTargetType(TargetTypes tTargetType) {
			foreach (TargetTypes t in ValidTargetTypes) {
				if (tTargetType == t) {
					return true;
				}
			}

			return false;
		}

		public virtual bool IsOnlyValidTargetTypeForItem(TargetTypes tTargetType) {
			return (ValidTargetTypes.Contains(tTargetType) && ValidTargetTypes.Count == 1);
		}

		//perhaps I should only have one version on this function (no parent) because there is a danger of duplicate events
		public bool IsValidTargetParent(ITarget tTarget) {
			if (IsValidTargetType(tTarget.ActiveTargetType) == false) {
				return false;
			}

			if (IsValidTarget(tTarget) == true) {
				if (GameState.InitialUltimatum(new TargetingEvent(tTarget, Controller)) == Redirect.Proceed) {
					return true;
				}
			}
			return false;
		}

		public virtual bool IsValidTarget(ITarget tTarget) {
			foreach (TargetTypes t in ValidTargetTypes) {
				if (tTarget.ActiveTargetType == t) {
					return true;
				}
			}

			return false;
		}

		public void OnGainParent(Wizard tHolder) {
			OnGainChild(tHolder);
			Carrier = tHolder;
		}

		public virtual void OnGainChild(Wizard tHolder) {
			//empty by default
		}

		public void OnLossParent(Wizard tDropper) {
			OnLossChild(tDropper);
			Carrier = null;
		}

		public virtual void OnLossChild(Wizard tDropper) {
			//empty by default
		}

		public void OnActivationParent() {
			OnActivationChild();
		}

		public virtual void OnActivationChild() {
			//empty by default
		}

		public virtual IItemUsage UseItem() {
			return null;
		}

		public void OnResolutionParent() {
			OnResolutionChild();
		}

		public virtual void OnResolutionChild() {
			//empty by default
		}

		//public int CardValue { get; set; }

		public double ShotDirection { get; set; }

		public override string ToString() {
			return GetType().Name;
		}

		public void Destroy(DestroyEffect destroyEffect) {
			if (Carrier != null) {
				Carrier.loseItem(this);
			}
			else {
				Location = null;
			}
		}

		public object Aimable {
			get {
				return this;
			}
		}

		public ITarget Target {
			get {
				return ItemTarget;
			}
			set {
				ItemTarget = value;
			}
		}

		public Wizard Controller { get; set; }

		public string Description { get; set; }

		public ICard OriginalCard { get; set; }
	}
}